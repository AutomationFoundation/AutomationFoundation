using System;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using AutomationFoundation.Runtime;
using AutomationFoundation.Runtime.Abstractions.Synchronization;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationFoundation.Features.ProducerConsumer
{
    /// <summary>
    /// Provides contextual information regarding the production and consumption of work being processed by the runtime.
    /// </summary>
    public class ProducerConsumerContext : ProcessingContext, IProducerConsumerContext
    {
        /// <summary>
        /// Gets the item which was produced.
        /// </summary>
        public IProducedItem Item { get; set; }

        /// <summary>
        /// Gets the scope associated with the produced item.
        /// </summary>
        public IServiceScope Scope { get; set; }

        /// <summary>
        /// Gets the synchronization lock for the produced item.
        /// </summary>
        public ISynchronizationLock SynchronizationLock { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProducerConsumerContext"/> class.
        /// </summary>
        public ProducerConsumerContext()
        {
        }
    }
}