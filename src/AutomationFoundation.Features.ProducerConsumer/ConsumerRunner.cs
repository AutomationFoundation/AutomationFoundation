using System;
using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using AutomationFoundation.Runtime;

namespace AutomationFoundation.Features.ProducerConsumer
{
    /// <summary>
    /// Provides an adapter which runs the consumer.
    /// </summary>
    /// <typeparam name="TItem">The type of object being consumed.</typeparam>
    public class ConsumerRunner<TItem> : IConsumerRunner<TItem>
    {
        private readonly IConsumer<TItem> consumer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsumerRunner{T}"/> class.
        /// </summary>
        /// <param name="consumer">The consumer being wrapped by this adapter instance.</param>
        public ConsumerRunner(IConsumer<TItem> consumer)
        {
            this.consumer = consumer ?? throw new ArgumentNullException(nameof(consumer));
        }

        /// <inheritdoc />
        public async Task RunAsync(ProducerConsumerContext<TItem> context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            try
            {
                ProcessingContext.SetCurrent(context);

                context.Consumer = consumer;

                try
                {
                    await consumer.ConsumeAsync(context);
                }
                finally
                {
                    ProcessingContext.Clear();
                }
            }
            finally
            {
                context.Dispose();
            }
        }
    }
}