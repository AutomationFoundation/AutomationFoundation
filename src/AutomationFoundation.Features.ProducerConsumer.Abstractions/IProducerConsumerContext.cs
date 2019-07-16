using AutomationFoundation.Runtime;
using AutomationFoundation.Runtime.Abstractions.Synchronization;

namespace AutomationFoundation.Features.ProducerConsumer.Abstractions
{
    /// <summary>
    /// Identifies an object which provides contextual information regarding the production and consumption of work being processed by the runtime.
    /// </summary>
    /// <typeparam name="TItem">The type of item which was produced.</typeparam>
    public interface IProducerConsumerContext<TItem> : IProcessingContext
    {
        /// <summary>
        /// Gets the item which was produced.
        /// </summary>
        TItem Item { get; }

        /// <summary>
        /// Gets the producer which produced the item.
        /// </summary>
        IProducer<TItem> Producer { get; }

        /// <summary>
        /// Gets the consumer which will consume the item.
        /// </summary>
        IConsumer<TItem> Consumer { get; }

        /// <summary>
        /// Gets the synchronization lock for the produced item.
        /// </summary>
        ISynchronizationLock SynchronizationLock { get; }
    }
}