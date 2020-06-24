using System.Threading.Tasks;

namespace AutomationFoundation.Runtime
{
    /// <summary>
    /// Identifies an object which can be started.
    /// </summary>
    public interface IStartable
    {
        /// <summary>
        /// Starts the object asynchronously.
        /// </summary>
        /// <returns>The asynchronous task.</returns>
        Task StartAsync();
    }
}