using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationFoundation.Features.ProducerConsumer.Factories
{
    /// <summary>
    /// Provides the default factory for creating producer objects.
    /// </summary>
    /// <typeparam name="TProducer">The type of producer to create.</typeparam>
    /// <typeparam name="TItem">The type of object being produced.</typeparam>
    public class DefaultProducerFactory<TProducer, TItem> : IProducerFactory<TItem>
        where TProducer : IProducer<TItem>, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultProducerFactory{TProducer, TItem}"/> class.
        /// </summary>
        public DefaultProducerFactory()
        {
        }

        /// <inheritdoc />
        public IProducer<TItem> Create(IServiceScope scope)
        {
            return new TProducer();
        }
    }
}