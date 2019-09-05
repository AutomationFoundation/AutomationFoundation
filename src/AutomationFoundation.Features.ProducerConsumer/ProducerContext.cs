using System;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;

namespace AutomationFoundation.Features.ProducerConsumer
{
    /// <summary>
    /// Contains contextual information regarding the production of work.
    /// </summary>
    /// <typeparam name="TItem">The type of item which was produced.</typeparam>
    public class ProducerContext<TItem>
    {
        /// <summary>
        /// Gets or sets the producer instance which produced the item.
        /// </summary>
        public IProducer<TItem> Producer { get; set; }

        /// <summary>
        /// Gets or sets the production strategy for producing the item.
        /// </summary>
        public IProducerExecutionStrategy<TItem> ExecutionStrategy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the item was produced.
        /// </summary>
        public DateTime ProducedOn { get; set; }

        /// <summary>
        /// Gets or sets the duration of time taken to produce the item.
        /// </summary>
        public TimeSpan? Duration { get; set; }
    }
}