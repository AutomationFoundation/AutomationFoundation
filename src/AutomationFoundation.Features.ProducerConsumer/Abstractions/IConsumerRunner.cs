using System.Threading;
using System.Threading.Tasks;

namespace AutomationFoundation.Features.ProducerConsumer.Abstractions
{
    /// <summary>
    /// Identifies an adapter which runs a consumer.
    /// </summary>
    public interface IConsumerRunner
    {
        /// <summary>
        /// Runs the consumer.
        /// </summary>
        /// <param name="context">The contextual information about the item produced.</param>
        /// <param name="cancellationToken">The cancellation token to monitor for cancellation requests.</param>
        Task Run(ProducerConsumerContext context, CancellationToken cancellationToken);
    }
}