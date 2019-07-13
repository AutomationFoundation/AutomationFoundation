using System;

namespace AutomationFoundation.Features.ProducerConsumer
{
    /// <summary>
    /// Contains contextual information for a producer engine.
    /// </summary>
    public struct ProducerEngineContext
    {
        /// <summary>
        /// Gets or sets the callback which will be executed when an item has been produced.
        /// </summary>
        public Action<ProducerConsumerContext> OnProducedCallback { get; set; }
    }
}