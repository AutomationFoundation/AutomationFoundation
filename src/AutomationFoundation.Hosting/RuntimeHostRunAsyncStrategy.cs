using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutomationFoundation.Hosting
{
    /// <summary>
    /// Provides a base asynchronous run strategy for the runtime.
    /// </summary>
    public abstract class RuntimeHostRunAsyncStrategy : IRuntimeHostRunAsyncStrategy, IDisposable
    {
        /// <summary>
        /// Gets the cancellation source used to stop the runtime.
        /// </summary>
        protected CancellationTokenSource CancellationSource { get; } = new CancellationTokenSource();

        /// <summary>
        /// Finalizes an instance of the <see cref="RuntimeHostRunAsyncStrategy"/> class.
        /// </summary>
        ~RuntimeHostRunAsyncStrategy()
        {
            Dispose(false);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources, false to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                CancellationSource.Dispose();
            }
        }

        /// <inheritdoc />
        public async Task RunAsync(IRuntimeHost host, int startupTimeoutMs, int shutdownTimeoutMs)
        {
            await AttachToListenForExitAsync();

            await RunCoreAsync(host, startupTimeoutMs, shutdownTimeoutMs);

            await OnStoppedAsync();
        }

        /// <summary>
        /// Attach the strategy to the mechanism which will be signaled to terminate the runtime.
        /// </summary>
        /// <returns>The task to await.</returns>
        protected abstract Task AttachToListenForExitAsync();

        /// <summary>
        /// Notified after the runtime has been stopped.
        /// </summary>
        /// <returns>The task to await.</returns>
        protected abstract Task OnStoppedAsync();

        private async Task RunCoreAsync(IRuntimeHost host, int startupTimeoutMs, int shutdownTimeoutMs)
        {
            try
            {
                await RunUntilSignaledAsync(host, startupTimeoutMs);
            }
            catch (OperationCanceledException)
            {
                // Swallow when cancellation has occurred.
                await OnCanceledWhileRunningAsync();
            }
            finally
            {
                await StopTheRuntimeHostAsync(host, shutdownTimeoutMs);
            }
        }

        /// <summary>
        /// Occurs when the runtime has been canceled while running.
        /// </summary>
        /// <returns>The task to await.</returns>
        protected virtual Task OnCanceledWhileRunningAsync()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Runs the host until signaled to terminate.
        /// </summary>
        /// <param name="host">The host to run.</param>
        /// <param name="startupTimeoutMs">The timeout (in milliseconds) upon which startup will be aborted.</param>
        /// <returns>The task to await.</returns>
        protected virtual async Task RunUntilSignaledAsync(IRuntimeHost host, int startupTimeoutMs)
        {
            using var startupCancellationSource = CancellationTokenSource.CreateLinkedTokenSource(CancellationSource.Token);

            startupCancellationSource.CancelAfter(startupTimeoutMs);

            await host.StartAsync(startupCancellationSource.Token);
            await Task.Delay(Timeout.InfiniteTimeSpan, CancellationSource.Token);
        }

        /// <summary>
        /// Stops the runtime host.
        /// </summary>
        /// <param name="host">The host to stop.</param>
        /// <param name="shutdownTimeoutMs">The timeout (in milliseconds) upon which shutdown will no longer be graceful, and shall be forced.</param>
        /// <returns>The task to await.</returns>
        protected virtual async Task StopTheRuntimeHostAsync(IRuntimeHost host, int shutdownTimeoutMs)
        {
            try
            {
                using var shutdownCancellationSource = new CancellationTokenSource(shutdownTimeoutMs);
                await host.StopAsync(shutdownCancellationSource.Token);
            }
            catch (OperationCanceledException)
            {
                // Swallow when the graceful shutdown period has expired.
                await OnCanceledWhileStoppingAsync();
            }
        }

        /// <summary>
        /// Occurs when the runtime has been canceled while stopping.
        /// </summary>
        /// <returns>The task to await.</returns>
        protected virtual Task OnCanceledWhileStoppingAsync()
        {
            return Task.CompletedTask;
        }
    }
}