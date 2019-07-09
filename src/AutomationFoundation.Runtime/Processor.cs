using System;
using AutomationFoundation.Runtime.Abstractions;

namespace AutomationFoundation.Runtime
{
    /// <summary>
    /// Represents a processor. This class must be inherited.
    /// </summary>
    public abstract class Processor : DisposableObject, IProcessor
    {
        /// <summary>
        /// Gets the object used for thread synchronization.
        /// </summary>
        protected object SyncRoot { get; } = new object();

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the processor state.
        /// </summary>
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
        /// Starts the processor.
        /// </summary>
        public void Start()
        {
            GuardMustNotBeDisposed();

            GuardMustNotHaveErrored();
            GuardMustNotAlreadyBeStarted();

            lock (SyncRoot)
            {
                GuardMustNotHaveErrored();
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

        /// <summary>
        /// Stops the processor.
        /// </summary>
        public void Stop()
        {
            GuardMustNotBeDisposed();

            GuardMustNotHaveErrored();
            GuardMustNotAlreadyBeStopped();

            lock (SyncRoot)
            {
                GuardMustNotHaveErrored();
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
        protected void GuardMustNotHaveErrored()
        {
            if (State <= ProcessorState.Error)
            {
                throw new RuntimeException("The processor has encountered an unrecoverable error and can no longer be started.");
            }
        }
    }
}