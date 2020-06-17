using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutomationFoundation.Runtime.Abstractions
{
    /// <summary>
    /// Identifies a runtime.
    /// </summary>
    public interface IRuntime : IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether the runtime any processors which are started.
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// Adds the processor to the runtime.
        /// </summary>
        /// <param name="processor">The processor to add.</param>
        /// <returns>true if the processor was added, otherwise false.</returns>
        bool Add(IProcessor processor);

        /// <summary>
        /// Removes the processor from the runtime.
        /// </summary>
        /// <param name="processor">The processor to remove.</param>
        /// <returns>true if the processor was removed, otherwise false.</returns>
        bool Remove(IProcessor processor);

        /// <summary>
        /// Starts the runtime.
        /// </summary>
        /// <param name="cancellationToken">Indicates when the start process has been aborted.</param>
        Task StartAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Stops the runtime.
        /// </summary>
        /// <param name="cancellationToken">Indicates when the stop process should no longer be graceful.</param>
        Task StopAsync(CancellationToken cancellationToken = default);
    }
}