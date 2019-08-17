using System;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationFoundation.Features.ProducerConsumer.Factories
{
    /// <summary>
    /// Provides a factory which always returns the same instance of the consumer.
    /// </summary>
    /// <typeparam name="TItem">The type of object being consumed.</typeparam>
    public class SingletonConsumerFactory<TItem> : IConsumerFactory<TItem>
    {
        private readonly IConsumer<TItem> consumer;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingletonConsumerFactory{TItem}"/> class.
        /// </summary>
        /// <param name="consumer">The consumer instance to use.</param>
        public SingletonConsumerFactory(IConsumer<TItem> consumer)
        {
            this.consumer = consumer ?? throw new ArgumentNullException(nameof(consumer));
        }

        /// <inheritdoc />
        public IConsumer<TItem> Create(IServiceScope scope)
        {
            if (scope == null)
            {
                throw new ArgumentNullException(nameof(scope));
            }

            return consumer;
        }
    }
}