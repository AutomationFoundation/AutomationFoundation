using System.Threading.Tasks;

namespace AutomationFoundation.Features.ProducerConsumer.Abstractions
{
    /// <summary>
    /// Identifies an adapter which runs a consumer.
    /// </summary>
    /// <typeparam name="TItem">The type of item being consumed.</typeparam>
    public interface IConsumerRunner<TItem>
    {
        /// <summary>
        /// Runs the consumer.
        /// </summary>
        /// <param name="context">The contextual information about the item produced.</param>
        Task Run(ProducerConsumerContext<TItem> context);
    }
}