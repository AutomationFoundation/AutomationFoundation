using Microsoft.Extensions.DependencyInjection;

namespace AutomationFoundation.Features.ProducerConsumer.Abstractions
{
    /// <summary>
    /// Identifies an object which can create an <see cref="IConsumer{TItem}"/>
    /// </summary>
    /// <typeparam name="TItem">The type of item being consumed.</typeparam>
    public interface IConsumerFactory<TItem>
    {
        /// <summary>
        /// Creates the factory.
        /// </summary>
        /// <param name="scope">The lifetime scope the consumer.</param>
        /// <returns>The consumer instance.</returns>
        IConsumer<TItem> Create(IServiceScope scope);
    }
}