using System.Collections.Generic;
using System.Linq;
using AutomationFoundation.Runtime.Abstractions;

namespace AutomationFoundation.Runtime
{
    /// <summary>
    /// Provides a runtime for the Automation Foundation framework.
    /// </summary>
    public sealed class AutomationRuntime : DisposableObject, IRuntime
    {
        private readonly ICollection<Processor> processors;

        /// <summary>
        /// Gets the collection of processors.
        /// </summary>
        public ICollection<Processor> Processors
        {
            get
            {
                GuardMustNotBeDisposed();

                return processors;
            }
        }

        /// <inheritdoc />
        public bool IsActive
        {
            get
            {
                GuardMustNotBeDisposed();

                return Processors.Any(o => o.State >= ProcessorState.Started);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutomationRuntime"/> class.
        /// </summary>
        public AutomationRuntime()
        {
            processors = new ProcessorCollection(this);
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
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var processor in Processors)
                {
                    processor.Dispose();
                }
            }

            base.Dispose(disposing);
        }
    }
}