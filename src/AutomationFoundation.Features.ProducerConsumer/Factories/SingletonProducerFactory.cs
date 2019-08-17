using System;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationFoundation.Features.ProducerConsumer.Factories
{
    /// <summary>
    /// Provides a factory which always returns the same instance of the producer.
    /// </summary>
    /// <typeparam name="TItem">The type of object being produced.</typeparam>
    public class SingletonProducerFactory<TItem> : IProducerFactory<TItem>
    {
        private readonly IProducer<TItem> producer;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingletonProducerFactory{TItem}"/> class.
        /// </summary>
        /// <param name="producer">The producer instance to use.</param>
        public SingletonProducerFactory(IProducer<TItem> producer)
        {
            this.producer = producer ?? throw new ArgumentNullException(nameof(producer));
        }

        /// <inheritdoc />
        public IProducer<TItem> Create(IServiceScope scope)
        {
            if (scope == null)
            {
                throw new ArgumentNullException(nameof(scope));
            }

            return producer;
        }
    }
}