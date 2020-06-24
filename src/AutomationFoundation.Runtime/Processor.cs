using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutomationFoundation.Runtime
{
    /// <summary>
    /// Represents a processor. This class must be inherited.
    /// </summary>
    public abstract class Processor : IProcessor
    {
        private readonly SemaphoreSlim syncRoot = new SemaphoreSlim(1);
        private bool disposed;

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public ProcessorState State { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Processor"/> class.
        /// </summary>
        /// <param name="name">The name of the processor.</param>
        protected Processor(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="Processor"/> class.
        /// </summary>
        ~Processor()
        {
            Dispose(false);
        }

        /// <summary>
        /// Starts the processor.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token which indicates when the start process has been aborted.</param>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            GuardMustNotBeDisposed();
            GuardMustNotHaveEncounteredAnError();

            if (!ShouldAttemptToStart())
            {
                return;
            }

            await syncRoot.WaitAsync(cancellationToken);

            try
            {
                State = ProcessorState.Starting;

                await OnStartAsync(cancellationToken);

                State = ProcessorState.Started;
            }
            catch (Exception)
            {
                State = ProcessorState.Error;
                throw;
            }
            finally
            {
                syncRoot.Release();
            }
        }

        /// <summary>
        /// Determines whether the processor should attempt to start.
        /// </summary>
        /// <returns>true if the processor should be started, otherwise false.</returns>
        private bool ShouldAttemptToStart()
        {
            return State < ProcessorState.Starting;
        }

        /// <summary>
        /// Occurs when the processor is being started.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token which indicates when the start process has been aborted.</param>
        protected abstract Task OnStartAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Stops the processor.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token which indicates when the stop process should no longer be graceful.</param>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            GuardMustNotBeDisposed();

            if (!ShouldAttemptToStop())
            {
                return;
            }

            await syncRoot.WaitAsync(cancellationToken);

            try
            {
                State = ProcessorState.Stopping;

                await OnStopAsync(cancellationToken);

                State = ProcessorState.Stopped;
            }
            catch (Exception)
            {
                State = ProcessorState.Error;
                throw;
            }
            finally
            {
                syncRoot.Release();
            }
        }

        /// <summary>
        /// Determines whether the processor should attempt to stop.
        /// </summary>
        /// <returns>true if the processor should attempt to stop, otherwise false.</returns>
        private bool ShouldAttemptToStop()
        {
            return State != ProcessorState.Error && State >= ProcessorState.Started;
        }

        /// <summary>
        /// Occurs when the processor is being stopped.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token which indicates when the stop process should no longer be graceful.</param>
        protected abstract Task OnStopAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Ensures the processor has not encountered an error.
        /// </summary>
        private void GuardMustNotHaveEncounteredAnError()
        {
            if (State <= ProcessorState.Error)
            {
                throw new RuntimeException("The processor has encountered an unrecoverable error and can no longer be started.");
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
                syncRoot.Dispose();
            }

            disposed = true;
        }

        /// <summary>
        /// Guards against the processor being used after disposal.
        /// </summary>
        protected void GuardMustNotBeDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(Processor));
            }
        }
    }
}