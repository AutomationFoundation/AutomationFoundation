using System;
using System.Threading.Tasks;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using AutomationFoundation.Runtime;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationFoundation.Features.ProducerConsumer
{
    /// <summary>
    /// Provides an adapter which runs the consumer.
    /// </summary>
    /// <typeparam name="TItem">The type of object being consumed.</typeparam>
    public class ConsumerRunner<TItem> : IConsumerRunner<TItem>
    {
        private readonly Func<IServiceScope, IConsumer<TItem>> consumerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsumerRunner{T}"/> class.
        /// </summary>
        /// <param name="consumerFactory">The consumer being wrapped by this adapter instance.</param>
        public ConsumerRunner(Func<IServiceScope, IConsumer<TItem>> consumerFactory)
        {
            this.consumerFactory = consumerFactory ?? throw new ArgumentNullException(nameof(consumerFactory));
        }

        /// <inheritdoc />
        public Task RunAsync(ProducerConsumerContext<TItem> context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return RunAsyncImpl(context);
        }

        private async Task RunAsyncImpl(ProducerConsumerContext<TItem> context)
        {
            try
            {
                ProcessingContext.SetCurrent(context);
                CreateConsumer(context);

                try
                {
                    await ConsumeAsync(context);
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

        /// <summary>
        /// Creates the consumer which will consume the object.
        /// </summary>
        /// <param name="context">The contextual information about the item being consumed.</param>
        protected virtual void CreateConsumer(ProducerConsumerContext<TItem> context)
        {
            context.Consumer = consumerFactory(context.ServiceScope);
        }

        /// <summary>
        /// Consumes the item.
        /// </summary>
        /// <param name="context">The contextual information about the item being consumed.</param>
        protected virtual async Task ConsumeAsync(ProducerConsumerContext<TItem> context)
        {
            await context.Consumer.ConsumeAsync(context);
        }
    }
}