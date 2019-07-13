using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutomationFoundation.Features.ProducerConsumer.Abstractions
{
    /// <summary>
    /// Identifies an adapter which runs a producer.
    /// </summary>
    public interface IProducerRunner
    {
        /// <summary>
        /// Runs the producer.
        /// </summary>
        /// <param name="onProducedCallback">The callback to execute if an object is produced.</param>
        /// <param name="cancellationToken">The cancellation token to monitor for cancellation requests.</param>
        /// <returns>true if an item was produced, otherwise false.</returns>
        Task<bool> Run(Action<ProducerConsumerContext> onProducedCallback, CancellationToken cancellationToken);
    }
}