using System.Threading;
using System.Threading.Tasks;

namespace AutomationFoundation.Features.ProducerConsumer.Abstractions
{
    /// <summary>
    /// Identifies a consumer of objects.
    /// </summary>
    /// <typeparam name="T">The type of objects being consumed.</typeparam>
    public interface IConsumer<in T>
    {
        /// <summary>
        /// Consumes the object.
        /// </summary>
        /// <param name="item">The object to consume.</param>
        /// <param name="cancellationToken">The cancellation token to monitor for cancellation requests.</param>
        /// <returns>The task to await for the consumption to complete.</returns>
        Task Consume(T item, CancellationToken cancellationToken);
    }
}