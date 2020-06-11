using System;
using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Hosting.Abstractions;

namespace AutomationFoundation.Hosting
{
    /// <summary>
    /// Contains extensions for the runtime host.
    /// </summary>
    public static class RuntimeHostExtensions
    {
        /// <summary>
        /// Defines the default timeout (in milliseconds) of the startup and shutdown procedures.
        /// </summary>
        private const int DefaultTimeoutMs = Timeout.Infinite;

        /// <summary>
        /// Runs until the cancellation token is signaled.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="cancellationToken">The cancellation token to monitor for cancellation requests.</param>
        /// <param name="startupTimeoutMs">The timeout (in milliseconds) upon which startup will be aborted.</param>
        /// <param name="shutdownTimeoutMs">The timeout (in milliseconds) upon which shutdown will no longer be graceful, and shall be forced.</param>
        /// <returns>The task to await.</returns>
        public static async Task RunAsync(this IRuntimeHost host, CancellationToken cancellationToken, int startupTimeoutMs = DefaultTimeoutMs, int shutdownTimeoutMs = DefaultTimeoutMs)
        {
            try
            {
                using var startupCancellationSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                startupCancellationSource.CancelAfter(startupTimeoutMs);

                await host.StartAsync(startupCancellationSource.Token);
                await Task.Delay(Timeout.InfiniteTimeSpan, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                // Swallow when cancellation has occurred.
            }
            finally
            {
                try
                {
                    using var shutdownCancellationSource = new CancellationTokenSource(shutdownTimeoutMs);
                    await host.StopAsync(shutdownCancellationSource.Token);
                }
                catch (OperationCanceledException)
                {
                    // Swallow when the graceful shutdown period has expired.
                }
            }
        }
    }
}