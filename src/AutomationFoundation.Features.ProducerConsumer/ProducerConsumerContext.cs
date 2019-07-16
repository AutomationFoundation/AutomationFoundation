using System;
using System.Diagnostics;
using System.Threading;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using AutomationFoundation.Runtime;
using AutomationFoundation.Runtime.Abstractions.Synchronization;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationFoundation.Features.ProducerConsumer
{
    /// <summary>
    /// Provides contextual information regarding the production and consumption of work being processed by the runtime.
    /// </summary>
    /// <typeparam name="TItem">The type of item which was produced.</typeparam>
    public class ProducerConsumerContext<TItem> : ProcessingContext, IProducerConsumerContext<TItem>
    {
        /// <summary>
        /// Gets or sets the item which was produced.
        /// </summary>
        public TItem Item { get; set; }

        /// <summary>
        /// Gets or sets the synchronization lock for the produced item.
        /// </summary>
        public ISynchronizationLock SynchronizationLock { get; set; }

        /// <summary>
        /// Gets or sets the processor.
        /// </summary>
        public ProducerConsumerProcessor<TItem> Processor { get; set; }

        /// <summary>
        /// Gets or sets the producer which produced the item.
        /// </summary>
        public IProducer<TItem> Producer { get; set; }

        /// <summary>
        /// Gets or sets the consumer which will consume the item.
        /// </summary>
        public IConsumer<TItem> Consumer { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProducerConsumerContext{TItem}"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the context.</param>
        /// <param name="serviceScope">The scope of the request used for dependency injection.</param>
        public ProducerConsumerContext(Guid id, IServiceScope serviceScope)
            : base(id, serviceScope)
        {
        }
    }
}