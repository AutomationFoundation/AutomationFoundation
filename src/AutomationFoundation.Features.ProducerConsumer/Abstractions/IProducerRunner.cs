using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutomationFoundation.Features.ProducerConsumer.Abstractions
{
    /// <summary>
    /// Identifies an adapter which runs a producer.
    /// </summary>
    /// <typeparam name="TItem">The type of item being produced.</typeparam>
    public interface IProducerRunner<TItem>
    {
        /// <summary>
        /// Runs the producer.
        /// </summary>
        /// <param name="onProducedCallback">The callback to execute if an object is produced.</param>
        /// <param name="cancellationToken">The cancellation token to monitor for cancellation requests.</param>
        /// <returns>true if an item was produced, otherwise false.</returns>
        Task<bool> Run(Action<ProducerConsumerContext<TItem>> onProducedCallback, CancellationToken cancellationToken);
    }
}