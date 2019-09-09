namespace AutomationFoundation.Features.ProducerConsumer.Abstractions
{
    /// <summary>
    /// Identifies a consumer engine.
    /// </summary>
    /// <typeparam name="TItem">The type of item being consumed.</typeparam>
    public interface IConsumerEngine<TItem>
    {
        /// <summary>
        /// Consumes the item.
        /// </summary>
        /// <param name="context">The object containing contextual information about an item which was produced.</param>
        void Consume(IProducerConsumerContext<TItem> context);
    }
}