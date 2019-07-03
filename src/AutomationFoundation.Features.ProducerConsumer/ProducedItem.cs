using System;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;

namespace AutomationFoundation.Features.ProducerConsumer
{
    /// <summary>
    /// Represents a produced item.
    /// </summary>
    public class ProducedItem : IProducedItem
    {
        private readonly object item;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProducedItem"/> class.
        /// </summary>
        /// <param name="item">The item which was produced.</param>      
        public ProducedItem(object item)
        {
            this.item = item;
        }

        /// <summary>
        /// Gets the item which was produced.
        /// </summary>
        /// <typeparam name="T">The type of object which was produced.</typeparam>
        /// <returns>The produced object.</returns>
        public T GetItem<T>()
        {
            if (!(item is T))
            {
                throw new InvalidCastException($"The type '{item.GetType().FullName}' does not support casting to: '{typeof(T).FullName}'.");
            }

            return (T)item;
        }
    }
}