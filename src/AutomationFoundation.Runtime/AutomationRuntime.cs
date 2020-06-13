using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Runtime.Abstractions;
using AutomationFoundation.Runtime.Threading;

namespace AutomationFoundation.Runtime
{
    /// <summary>
    /// Provides a runtime for the Automation Foundation framework.
    /// </summary>
    public sealed class AutomationRuntime : IRuntime
    {
        private readonly SemaphoreSlim @lock = new SemaphoreSlim(1);
        private readonly IList<IProcessor> processors = new List<IProcessor>();

        private bool disposed;

        /// <summary>
        /// Gets the collection of processors.
        /// </summary>
        public IEnumerable<IProcessor> Processors => new ReadOnlyCollection<IProcessor>(processors);

        /// <inheritdoc />
        public bool IsRunning => processors.Any(o => o.State >= ProcessorState.Started);

        /// <summary>
        /// Finalizes an instance of the <see cref="AutomationRuntime"/> class.
        /// </summary>
        ~AutomationRuntime()
        {
            Dispose(false);
        }

        /// <inheritdoc />
        public bool Add(IProcessor processor)
        {
            if (processor == null)
            {
                throw new ArgumentNullException(nameof(processor));
            }

            GuardMustNotBeDisposed();

            @lock.Wait();

            try
            {
                if (!processors.Contains(processor))
                {
                    processors.Add(processor);
                    return true;
                }
            }
            finally
            {
                @lock.Release();
            }

            return false;
        }

        /// <inheritdoc />
        public bool Remove(IProcessor processor)
        {
            if (processor == null)
            {
                throw new ArgumentNullException(nameof(processor));
            }

            GuardMustNotBeDisposed();

            @lock.Wait();

            try
            {
                if (processors.Contains(processor))
                {
                    processors.Remove(processor);
                    return true;
                }
            }
            finally
            {
                @lock.Release();
            }

            return false;
        }

        /// <inheritdoc />
        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            GuardMustNotBeDisposed();

            var tasks = new List<Task>();

            await @lock.WaitAsync(cancellationToken);

            try
            {
                foreach (var processor in processors)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    var task = Task.Run(async () =>
                    {
                        await processor.StartAsync(cancellationToken);
                    }, CancellationToken.None);

                    tasks.Add(task);
                }
            }
            finally
            {
                @lock.Release();
            }

            await Task.WhenAll(tasks);
        }

        /// <inheritdoc />
        public async Task StopAsync(CancellationToken cancellationToken = default)
        {
            GuardMustNotBeDisposed();

            var tasks = new List<Task>();

            await @lock.WaitAsync(cancellationToken);

            try
            {
                foreach (var processor in processors)
                {
                    var task = Task.Run(async () =>
                    {
                        try
                        {
                            var stopTask = processor.StopAsync(cancellationToken);
                            await stopTask.AbandonWhen(cancellationToken);
                        }
                        catch (TaskAbandonedException)
                        {
                            // TODO: This needs to get logged which process was abandoned.
                        }
                    }, CancellationToken.None);

                    tasks.Add(task);
                }
            }
            finally
            {
                @lock.Release();
            }

            await Task.WhenAll(tasks);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var processor in Processors)
                {
                    processor.Dispose();
                }

                @lock.Dispose();

                disposed = true;
            }
        }

        private void GuardMustNotBeDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(AutomationRuntime));
            }
        }
    }
}