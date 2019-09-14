using System.Threading.Tasks;

namespace AutomationFoundation.Runtime.Abstractions
{
    /// <summary>
    /// Identifies an object which can be stopped.
    /// </summary>
    public interface IStoppable
    {
        /// <summary>
        /// Stops the object asynchronously.
        /// </summary>
        /// <returns>The task to await.</returns>
        Task StopAsync();

        /// <summary>
        /// Waits for the engine to stop.
        /// </summary>
        /// <returns>The task to await.</returns>
        Task WaitForCompletionAsync();
    }
}