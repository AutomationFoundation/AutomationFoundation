namespace AutomationFoundation.Features.ProducerConsumer.Abstractions
{
    /// <summary>
    /// Identifies an object which was produced by a producer engine.
    /// </summary>
    public interface IProducedItem
    {
        /// <summary>
        /// Gets the object which was produced.
        /// </summary>
        /// <typeparam name="T">The type of object produced.</typeparam>
        /// <returns>The produced object.</returns>
        T GetItem<T>();
    }
}