using System.Threading;
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
        /// <param name="cancellationToken">Indicates when the start process has been aborted.</param>
        /// <returns>The asynchronous task.</returns>
        Task StartAsync(CancellationToken cancellationToken);
    }
}