using System.Threading;

namespace AutomationFoundation.Features.ProducerConsumer.Abstractions
{
    /// <summary>
    /// Identifies a consumer engine.
    /// </summary>
    public interface IConsumerEngine
    {
        /// <summary>
        /// Initializes the engine.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to monitor for cancellation requests.</param>
        void Initialize(CancellationToken cancellationToken);

        /// <summary>
        /// Consumes the item.
        /// </summary>
        /// <param name="context">The object containing contextual information about an item which was produced.</param>
        void Consume(ProducerConsumerContext context);
    }
}