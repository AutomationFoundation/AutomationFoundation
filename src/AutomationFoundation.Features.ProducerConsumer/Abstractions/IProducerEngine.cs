using System.Threading;
using AutomationFoundation.Runtime.Abstractions;

namespace AutomationFoundation.Features.ProducerConsumer.Abstractions
{
    /// <summary>
    /// Identifies a producer engine.
    /// </summary>
    public interface IProducerEngine : IStartable<ProducerEngineContext>, IStoppable
    {
        /// <summary>
        /// Initializes the engine.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to monitor for cancellation requests.</param>
        void Initialize(CancellationToken cancellationToken);
    }
}