using System;
using AutomationFoundation.Runtime.Abstractions;

namespace AutomationFoundation.Runtime
{
    /// <summary>
    /// Represents a processor. This class must be inherited.
    /// </summary>
    public abstract class Processor : IProcessor
    {
        private bool disposed;

        /// <summary>
        /// Gets the object used for thread synchronization.
        /// </summary>
        protected object SyncRoot { get; } = new object();

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

        /// <inheritdoc />
        public void Start()
        {
            GuardMustNotBeDisposed();

            GuardMustNotHaveEncounteredAnError();
            GuardMustNotAlreadyBeStarted();

            lock (SyncRoot)
            {
                GuardMustNotHaveEncounteredAnError();
                GuardMustNotAlreadyBeStarted();

                try
                {
                    State = ProcessorState.Starting;
                    
                    OnStart();

                    State = ProcessorState.Started;
                }
                catch (Exception)
                {
                    State = ProcessorState.Error;
                    throw;
                }
            }
        }

        /// <summary>
        /// Ensures the processor has not already been started.
        /// </summary>
        protected void GuardMustNotAlreadyBeStarted()
        {
            if (State >= ProcessorState.Started)
            {
                throw new RuntimeException("The processor has already been started.");
            }
        }

        /// <summary>
        /// Occurs when the processor is being started.
        /// </summary>
        protected abstract void OnStart();

        /// <inheritdoc />
        public void Stop()
        {
            GuardMustNotBeDisposed();

            GuardMustNotHaveEncounteredAnError();
            GuardMustNotAlreadyBeStopped();

            lock (SyncRoot)
            {
                GuardMustNotHaveEncounteredAnError();
                GuardMustNotAlreadyBeStopped();

                try
                {
                    State = ProcessorState.Stopping;

                    OnStop();

                    State = ProcessorState.Stopped;
                }
                catch (Exception)
                {
                    State = ProcessorState.Error;
                    throw;
                }
            }
        }        

        /// <summary>
        /// Ensures the processor has not already been stopped.
        /// </summary>
        protected void GuardMustNotAlreadyBeStopped()
        {
            if (State >= ProcessorState.Stopping && State <= ProcessorState.Stopped)
            {
                throw new RuntimeException("The processor has not been started.");
            }
        }

        /// <summary>
        /// Occurs when the processor is being stopped.
        /// </summary>
        protected abstract void OnStop();

        /// <summary>
        /// Ensures the processor has not encountered an error.
        /// </summary>
        protected void GuardMustNotHaveEncounteredAnError()
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
            disposed = true;
        }

        /// <summary>
        /// Guards against the processor being used after disposal.
        /// </summary>
        protected void GuardMustNotBeDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(AutomationRuntime));
            }
        }
    }
}