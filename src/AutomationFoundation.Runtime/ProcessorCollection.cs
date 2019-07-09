using System;
using System.Collections.ObjectModel;
using AutomationFoundation.Runtime.Abstractions;

namespace AutomationFoundation.Runtime
{
    /// <summary>
    /// Represents a collection of processors.
    /// </summary>
    public sealed class ProcessorCollection : Collection<IProcessor>
    {
        private readonly IRuntime parent;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessorCollection"/> class.
        /// </summary>
        /// <param name="parent">The runtime which will own this collection of processors.</param>
        public ProcessorCollection(IRuntime parent)
        {
            this.parent = parent ?? throw new ArgumentNullException(nameof(parent));
        }

        /// <inheritdoc />
        protected override void ClearItems()
        {
            GuardMustNotBeActive();

            base.ClearItems();
        }

        /// <inheritdoc />
        protected override void InsertItem(int index, IProcessor item)
        {
            if (Contains(item))
            {
                throw new ArgumentException("The processor already exists.");
            }

            GuardMustNotBeActive();
            base.InsertItem(index, item);
        }

        /// <inheritdoc />
        protected override void RemoveItem(int index)
        {
            GuardMustNotBeActive();

            base.RemoveItem(index);
        }

        /// <inheritdoc />
        protected override void SetItem(int index, IProcessor item)
        {
            GuardMustNotBeActive();

            base.SetItem(index, item);
        }

        private void GuardMustNotBeActive()
        {
            if (!parent.IsActive)
            {
                return;
            }

            throw new NotSupportedException(
                "The runtime cannot change processors while running. Please stop the runtime and try again.");
        }
    }
}