using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AutomationFoundation.Runtime.Abstractions;

namespace AutomationFoundation.Runtime
{
    /// <summary>
    /// Provides a runtime for the Automation Foundation framework.
    /// </summary>
    public sealed class AutomationRuntime : IRuntime
    {
        private readonly object syncRoot = new object();
        private readonly IList<IProcessor> processors = new List<IProcessor>();

        private bool disposed;

        /// <summary>
        /// Gets the collection of processors.
        /// </summary>
        public IEnumerable<IProcessor> Processors => new ReadOnlyCollection<IProcessor>(processors);

        /// <inheritdoc />
        public bool IsActive => processors.Any(o => o.State >= ProcessorState.Started);

        /// <summary>
        /// Initializes a new instance of the <see cref="AutomationRuntime"/> class.
        /// </summary>
        public AutomationRuntime()
        {
        }

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

            lock (syncRoot)
            {
                GuardMustNotBeDisposed();

                if (!processors.Contains(processor))
                {
                    processors.Add(processor);
                    return true;
                }

                return false;
            }
        }

        /// <inheritdoc />
        public bool Remove(IProcessor processor)
        {
            if (processor == null)
            {
                throw new ArgumentNullException(nameof(processor));
            }

            GuardMustNotBeDisposed();

            lock (syncRoot)
            {
                GuardMustNotBeDisposed();

                if (processors.Contains(processor))
                {
                    processors.Remove(processor);
                    return true;
                }

                return false;
            }
        }

        /// <inheritdoc />
        public void Start()
        {
            GuardMustNotBeDisposed();
            
            lock (syncRoot)
            {
                GuardMustNotBeDisposed();

                foreach (var processor in Processors)
                {
                    processor.Start();
                }
            }
        }

        /// <inheritdoc />
        public void Stop()
        {
            GuardMustNotBeDisposed();

            lock (syncRoot)
            {
                foreach (var processor in Processors)
                {
                    processor.Stop();
                }
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
                lock (syncRoot)
                {
                    foreach (var processor in Processors)
                    {
                        processor.Dispose();
                    }

                    disposed = true;
                }
            }
        }

        private void GuardMustNotBeDisposed()
        {
            lock (syncRoot)
            {
                if (disposed)
                {
                    throw new ObjectDisposedException(nameof(AutomationRuntime));
                }
            }
        }
    }
}