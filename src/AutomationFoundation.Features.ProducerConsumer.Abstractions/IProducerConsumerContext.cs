using AutomationFoundation.Runtime;

namespace AutomationFoundation.Features.ProducerConsumer.Abstractions
{
    /// <summary>
    /// Identifies an object which provides contextual information regarding the production and consumption of work being processed by the runtime.
    /// </summary>
    /// <typeparam name="TItem">The type of item which was produced.</typeparam>
    public interface IProducerConsumerContext<out TItem> : IProcessingContext
    {
        /// <summary>
        /// Gets the item which was produced.
        /// </summary>
        TItem Item { get; }
    }
}