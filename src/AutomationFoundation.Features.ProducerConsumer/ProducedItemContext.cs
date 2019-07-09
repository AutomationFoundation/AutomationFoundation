using System;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using AutomationFoundation.Runtime;
using AutomationFoundation.Runtime.Abstractions.Synchronization;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationFoundation.Features.ProducerConsumer
{
    /// <summary>
    /// Provides contextual information related to an item which was produced.
    /// </summary>
    public class ProducedItemContext : ProcessingContext
    {
        /// <summary>
        /// Gets the item which was produced.
        /// </summary>
        public IProducedItem Item { get; }

        /// <summary>
        /// Gets the scope associated with the produced item.
        /// </summary>
        public IServiceScope Scope { get; }

        /// <summary>
        /// Gets the synchronization lock for the produced item.
        /// </summary>
        public ISynchronizationLock SynchronizationLock { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProducedItemContext"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the item.</param>
        /// <param name="item">The item which was produced.</param>
        /// <param name="scope">The scope associated with the produced item.</param>
        /// <param name="synchronizationLock">The synchronization lock in use for the produced item.</param>
        public ProducedItemContext(Guid id, IProducedItem item, IServiceScope scope, ISynchronizationLock synchronizationLock)
            : base(id)
        {
            Item = item ?? throw new ArgumentNullException(nameof(item));
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
            SynchronizationLock = synchronizationLock;
        }
    }
}