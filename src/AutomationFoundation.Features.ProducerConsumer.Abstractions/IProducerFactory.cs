using Microsoft.Extensions.DependencyInjection;

namespace AutomationFoundation.Features.ProducerConsumer.Abstractions
{
    /// <summary>
    /// Identifies an object which can create an <see cref="IProducer{TItem}"/>
    /// </summary>
    /// <typeparam name="TItem">The type of item being produced.</typeparam>
    public interface IProducerFactory<TItem>
    {
        /// <summary>
        /// Creates the factory.
        /// </summary>
        /// <param name="scope">The lifetime scope the producer.</param>
        /// <returns>The producer instance.</returns>
        IProducer<TItem> Create(IServiceScope scope);
    }
}