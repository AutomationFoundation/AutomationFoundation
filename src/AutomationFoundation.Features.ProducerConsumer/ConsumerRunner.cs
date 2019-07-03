using System;
using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;

namespace AutomationFoundation.Features.ProducerConsumer
{
    /// <summary>
    /// Provides an adapter which runs the consumer.
    /// </summary>
    /// <typeparam name="T">The type of object being consumed.</typeparam>
    public class ConsumerRunner<T> : IConsumerRunner
    {
        private readonly IConsumer<T> consumer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsumerRunner{T}"/> class.
        /// </summary>
        /// <param name="consumer">The consumer being wrapped by this adapter instance.</param>
        public ConsumerRunner(IConsumer<T> consumer)
        {
            this.consumer = consumer ?? throw new ArgumentNullException(nameof(consumer));
        }

        /// <inheritdoc />
        public async Task Run(ProducedItemContext context, CancellationToken cancellationToken)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            try
            {
                ProcessingContext.SetCurrent(context);

                try
                {
                    await consumer.Consume(context.Item.GetItem<T>(), cancellationToken);
                }
                finally
                {
                    ProcessingContext.Clear();
                }
            }
            finally
            {
                context.SynchronizationLock?.Release();
            }
        }
    }
}