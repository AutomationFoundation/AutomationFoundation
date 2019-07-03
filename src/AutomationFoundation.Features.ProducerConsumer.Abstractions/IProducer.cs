using System.Threading;
using System.Threading.Tasks;

namespace AutomationFoundation.Features.ProducerConsumer.Abstractions
{
    /// <summary>
    /// Identifies a producer of objects.
    /// </summary>
    /// <typeparam name="T">The type of objects to produce.</typeparam>
    public interface IProducer<T>
    {
        /// <summary>
        /// Produces an object asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to monitor for cancellation requests.</param>
        /// <returns>The task which will return the object produced.</returns>
        Task<T> ProduceAsync(CancellationToken cancellationToken);
    }
}