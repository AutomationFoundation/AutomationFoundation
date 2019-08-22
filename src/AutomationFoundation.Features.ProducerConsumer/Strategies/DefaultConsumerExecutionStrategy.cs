﻿using System;
using System.Threading.Tasks;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using AutomationFoundation.Runtime;

namespace AutomationFoundation.Features.ProducerConsumer.Strategies
{
    /// <summary>
    /// Provides the default execution strategy for <see cref="IConsumer{TItem}"/> instances.
    /// </summary>
    /// <typeparam name="TItem">The type of object being consumed.</typeparam>
    public class DefaultConsumerExecutionStrategy<TItem> : IConsumerExecutionStrategy<TItem>
    {
        private readonly IConsumerFactory<TItem> consumerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultConsumerExecutionStrategy{TItem}"/> class.
        /// </summary>
        /// <param name="consumerFactory">The factory for creating consumers.</param>
        public DefaultConsumerExecutionStrategy(IConsumerFactory<TItem> consumerFactory)
        {
            this.consumerFactory = consumerFactory ?? throw new ArgumentNullException(nameof(consumerFactory));
        }

        /// <inheritdoc />
        public Task ExecuteAsync(ProducerConsumerContext<TItem> context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return ExecuteAsyncImpl(context);
        }

        private async Task ExecuteAsyncImpl(ProducerConsumerContext<TItem> context)
        {
            try
            {
                OnStarted(context);

                try
                {
                    CreateConsumer(context);
                    await ConsumeAsync(context);
                }
                finally
                {
                    OnCompleted(context);
                }
            }
            finally
            {
                OnExit(context);
            }
        }

        /// <summary>
        /// Occurs when the strategy is starting to execute.
        /// </summary>
        /// <param name="context">The contextual information about the item produced.</param>
        protected virtual void OnStarted(ProducerConsumerContext<TItem> context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.ConsumptionStrategy = this;
            ProcessingContext.SetCurrent(context);
        }

        /// <summary>
        /// Occurs when the strategy has completed execution.
        /// </summary>
        /// <param name="context">The contextual information about the item produced.</param>
        protected virtual void OnCompleted(ProducerConsumerContext<TItem> context)
        {
            ProcessingContext.Clear();
        }

        /// <summary>
        /// Occurs when the strategy is exiting.
        /// </summary>
        /// <param name="context">The contextual information about the item produced.</param>
        protected virtual void OnExit(ProducerConsumerContext<TItem> context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Dispose();
        }

        /// <summary>
        /// Creates the consumer which will consume the object.
        /// </summary>
        /// <param name="context">The contextual information about the item being consumed.</param>
        protected virtual void CreateConsumer(ProducerConsumerContext<TItem> context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var consumer = consumerFactory.Create(context.LifetimeScope);
            if (consumer == null)
            {
                throw new InvalidOperationException("The consumer was not created.");
            }

            context.Consumer = consumer;
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