using System;
using System.Threading.Tasks;
using AutomationFoundation.Runtime.Abstractions.Threading.Primitives;

namespace AutomationFoundation.Runtime.Threading.Primitives;

/// <summary>
/// Provides a mechanism to process a single unit of work.
/// </summary>
public class Worker : IRuntimeWorker
{
    private readonly object syncRoot = new object();

    private Task runTask;
    private Task postCompletionTask;
    private bool disposed;

    /// <summary>
    /// Gets a value indicating whether the worker has been initialized.
    /// </summary>
    public virtual bool Initialized { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the worker is currently running.
    /// </summary>
    public virtual bool IsRunning => (runTask != null && runTask.IsRunning()) ||
                             (postCompletionTask != null && postCompletionTask.IsRunning());

    /// <summary>
    /// Finalizes an instance of the <see cref="Worker"/> class.
    /// </summary>
    ~Worker()
    {
        Dispose(false);
    }

    /// <inheritdoc />
    public virtual void Initialize(WorkerExecutionContext context)
    {
        lock (syncRoot)
        {
            GuardMustNotBeDisposed();
            GuardMustNotBeRunning();
            GuardMustNotBeInitialized();

            runTask = new Task(() => OnRun(context), DetermineCreationOptions(context));
            if (context.PostCompletedCallback != null)
            {
                postCompletionTask = runTask.ContinueWith((t1, state) => OnPostCompleted(context), null);
            }

            Initialized = true;
        }
    }

    private void GuardMustNotBeInitialized()
    {
        if (Initialized)
        {
            throw new InvalidOperationException("The worker has already been initialized.");
        }
    }

    /// <summary>
    /// Determines the creation options for the task.
    /// </summary>
    /// <param name="context">The object containing contextual information for the worker execution.</param>
    /// <returns>The task creation options for the new task.</returns>
    protected virtual TaskCreationOptions DetermineCreationOptions(WorkerExecutionContext context)
    {
        return TaskCreationOptions.None;
    }

    private void GuardMustNotBeRunning()
    {
        if (IsRunning)
        {
            throw new InvalidOperationException("The worker is already in use.");
        }
    }

    /// <summary>
    /// Occurs when the worker runs.
    /// </summary>
    /// <param name="context">The object containing contextual information about the worker.</param>
    protected virtual void OnRun(WorkerExecutionContext context)
    {
        context.OnRunCallback?.Invoke();
    }

    /// <summary>
    /// Occurs when the worker has completed.
    /// </summary>
    /// <param name="context">The object containing contextual information about the worker.</param>
    protected virtual void OnPostCompleted(WorkerExecutionContext context)
    {
        context.PostCompletedCallback?.Invoke();
    }

    /// <inheritdoc />
    public virtual Task RunAsync()
    {
        GuardMustNotBeDisposed();
        GuardMustBeInitialized();

        Start();
        return WaitForCompletionAsync();
    }

    /// <inheritdoc />
    public virtual Task WaitForCompletionAsync()
    {
        GuardMustNotBeDisposed();

        if (ShouldWaitForThePostCompletionTask())
        {
            return Task.WhenAll(runTask, postCompletionTask);
        }

        return Task.WhenAll(runTask);
    }

    private bool ShouldWaitForThePostCompletionTask()
    {
        return postCompletionTask != null;
    }

    private void GuardMustBeInitialized()
    {
        if (!Initialized)
        {
            throw new InvalidOperationException("The worker has not been initialized.");
        }
    }

    private void Start()
    {
        runTask.Start();
    }

    /// <inheritdoc />
    public virtual void Run()
    {
        GuardMustNotBeDisposed();
        GuardMustBeInitialized();

        Start();
        WaitForCompletion();
    }

    /// <inheritdoc />
    public virtual void WaitForCompletion()
    {
        GuardMustNotBeDisposed();

        if (ShouldWaitForThePostCompletionTask())
        {
            Task.WaitAll(runTask, postCompletionTask);
        }
        else
        {
            Task.WaitAll(runTask);
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources, otherwise false to release unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Reset();
        }

        disposed = true;
    }

    /// <inheritdoc />
    public virtual void Reset()
    {
        if (!Initialized)
        {
            return;
        }

        lock (syncRoot)
        {
            runTask?.DisposeIfNecessary();
            postCompletionTask?.DisposeIfNecessary();

            Initialized = false;
        }
    }

    /// <summary>
    /// Guards against the worker cache having been disposed.
    /// </summary>
    protected void GuardMustNotBeDisposed()
    {
        if (disposed)
        {
            throw new ObjectDisposedException(nameof(Worker));
        }
    }
}