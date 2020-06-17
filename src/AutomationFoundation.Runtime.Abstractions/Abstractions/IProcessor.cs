using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutomationFoundation.Runtime.Abstractions
{
    /// <summary>
    /// Identifies a processor.
    /// </summary>
    public interface IProcessor : IDisposable
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the processor state.
        /// </summary>
        ProcessorState State { get; }

        /// <summary>
        /// Starts the processor.
        /// </summary>
        /// <param name="cancellationToken">Indicates when the start process has been aborted.</param>
        Task StartAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Stops the processor.
        /// </summary>
        /// <param name="cancellationToken">Indicates when the stop process should no longer be graceful.</param>
        Task StopAsync(CancellationToken cancellationToken);
    }
}