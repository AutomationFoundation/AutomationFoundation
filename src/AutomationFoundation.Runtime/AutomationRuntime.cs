using System;
using System.Collections.Generic;
using System.Linq;
using AutomationFoundation.Runtime.Abstractions;

namespace AutomationFoundation.Runtime
{
    /// <summary>
    /// Provides a runtime for the Automation Foundation framework.
    /// </summary>
    public sealed class AutomationRuntime : IRuntime
    {
        private bool disposed;

        /// <summary>
        /// Gets the collection of processors.
        /// </summary>
        public ICollection<IProcessor> Processors { get; }

        /// <inheritdoc />
        public bool IsActive => Processors.Any(o => o.State >= ProcessorState.Started);

        /// <summary>
        /// Initializes a new instance of the <see cref="AutomationRuntime"/> class.
        /// </summary>
        public AutomationRuntime()
        {
            Processors = new ProcessorCollection(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="AutomationRuntime"/> class.
        /// </summary>
        ~AutomationRuntime()
        {
            Dispose(false);
        }

        /// <inheritdoc />
        public void Start()
        {
            GuardMustNotBeDisposed();

            foreach (var processor in Processors)
            {
                processor.Start();
            }
        }

        /// <inheritdoc />
        public void Stop()
        {
            GuardMustNotBeDisposed();

            foreach (var processor in Processors)
            {
                processor.Stop();
            }
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
            }

            disposed = true;
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