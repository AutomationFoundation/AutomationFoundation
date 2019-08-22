using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationFoundation.Features.ProducerConsumer.Factories
{
    /// <summary>
    /// Provides a factory which always returns the same instance of the consumer.
    /// </summary>
    /// <typeparam name="TConsumer">The type of consumer to create.</typeparam>
    /// <typeparam name="TItem">The type of object being consumed.</typeparam>
    public class DefaultConsumerFactory<TConsumer, TItem> : IConsumerFactory<TItem>
        where TConsumer: IConsumer<TItem>, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultConsumerFactory{TConsumer, TItem}"/> class.
        /// </summary>
        public DefaultConsumerFactory()
        {
        }

        /// <inheritdoc />
        public IConsumer<TItem> Create(IServiceScope scope)
        {
            return new TConsumer();
        }
    }
}