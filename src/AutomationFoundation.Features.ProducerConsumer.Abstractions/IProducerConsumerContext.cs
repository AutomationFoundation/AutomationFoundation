using AutomationFoundation.Runtime;

namespace AutomationFoundation.Features.ProducerConsumer
{
    /// <summary>
    /// Identifies an object which provides contextual information regarding the production and consumption of work being processed by the runtime.
    /// </summary>
    public interface IProducerConsumerContext : IProcessingContext
    {
    }
}