using System.Threading;
using System.Threading.Tasks;

namespace AutomationFoundation.Runtime
{
    /// <summary>
    /// Identifies an object which can be stopped.
    /// </summary>
    public interface IStoppable
    {
        /// <summary>
        /// Stops the object asynchronously.
        /// </summary>
        /// <param name="cancellationToken">Indicates when the stop process should no longer be graceful.</param>
        /// <returns>The task to await.</returns>
        Task StopAsync(CancellationToken cancellationToken);
    }
}