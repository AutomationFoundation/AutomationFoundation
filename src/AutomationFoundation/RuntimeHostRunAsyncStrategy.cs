using System;
using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Hosting;
using Microsoft.Extensions.Options;

namespace AutomationFoundation
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
        /// Gets the options.
        /// </summary>
        public RuntimeHostRunAsyncOptions Options { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuntimeHostRunAsyncStrategy"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        protected RuntimeHostRunAsyncStrategy(IOptions<RuntimeHostRunAsyncOptions> options)
        {
            Options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

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
        public async Task RunAsync(IRuntimeHost host)
        {
            await AttachToListenForExitAsync();

            await RunCoreAsync(host);

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

        private async Task RunCoreAsync(IRuntimeHost host)
        {
            try
            {
                await RunUntilSignaledAsync(host);
            }
            catch (OperationCanceledException)
            {
                // Swallow when cancellation has occurred.
                await OnCanceledWhileRunningAsync();
            }
            finally
            {
                await StopTheRuntimeHostAsync(host);
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
        /// <returns>The task to await.</returns>
        protected virtual async Task RunUntilSignaledAsync(IRuntimeHost host)
        {
            using var startupCancellationSource = CancellationTokenSource.CreateLinkedTokenSource(CancellationSource.Token);
            startupCancellationSource.CancelAfter(Options.StartupTimeoutMs);

            await host.StartAsync(startupCancellationSource.Token);
            await DelayUntilSignaledAsync();
        }

        /// <summary>
        /// Delays until the strategy has been signaled for cancellation.
        /// </summary>
        /// <returns>The task to await.</returns>
        protected virtual async Task DelayUntilSignaledAsync()
        {
            await Task.Delay(Timeout.InfiniteTimeSpan, CancellationSource.Token);
        }

        /// <summary>
        /// Stops the runtime host.
        /// </summary>
        /// <param name="host">The host to stop.</param>
        /// <returns>The task to await.</returns>
        protected virtual async Task StopTheRuntimeHostAsync(IRuntimeHost host)
        {
            try
            {
                using var shutdownCancellationSource = new CancellationTokenSource(Options.ShutdownTimeoutMs);
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