using System.Threading.Tasks;
using AutomationFoundation.Features.ProducerConsumer.Abstractions;
using AutomationFoundation.Runtime.Abstractions;

namespace AutomationFoundation.Features.ProducerConsumer
{
    /// <summary>
    /// Contains extensions for the consumer engine.
    /// </summary>
    public static class ConsumerEngineExtensions
    {
        /// <summary>
        /// Optionally starts the engine if the engine is an asynchronous consumer engine.
        /// </summary>
        /// <param name="engine">The consumer engine.</param>
        /// <returns>The task being used to start the engine, otherwise a completed task if no task was required.</returns>
        public static async Task StartIfAsynchronous<TItem>(this IConsumerEngine<TItem> engine)
        {
            if (engine is IStartable startable)
            {
                await startable.StartAsync();
            }
        }

        /// <summary>
        /// Optionally stops the engine if the engine is an asynchronous consumer engine.
        /// </summary>
        /// <param name="engine">The consumer engine.</param>
        /// <returns>The task being used to stop the engine, otherwise a completed task if no task was required.</returns>
        public static async Task StopIfAsynchronous<TItem>(this IConsumerEngine<TItem> engine)
        {
            if (engine is IStoppable stoppable)
            {
                await stoppable.StopAsync();
            }
        }
    }
}