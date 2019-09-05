using System;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;

namespace AutomationFoundation.Features.ProducerConsumer
{
    /// <summary>
    /// Contains contextual information regarding the consumption of work.
    /// </summary>
    /// <typeparam name="TItem">The type of item which is being consumed.</typeparam>
    public class ConsumerContext<TItem>
    {
        /// <summary>
        /// Gets or sets the consumer which will consume the item.
        /// </summary>
        public IConsumer<TItem> Consumer { get; set; }

        /// <summary>
        /// Gets or sets the consumption strategy for consuming the item.
        /// </summary>
        public IConsumerExecutionStrategy<TItem> ExecutionStrategy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the item was produced.
        /// </summary>
        public DateTime ConsumedOn { get; set; }

        /// <summary>
        /// Gets or sets the duration of time taken to produce the item.
        /// </summary>
        public TimeSpan? Duration { get; set; }
    }
}