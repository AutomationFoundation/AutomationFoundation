using System;
using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Hosting.Abstractions;

namespace AutomationFoundation.Hosting
{
    /// <summary>
    /// Provides a run strategy which runs the host until CTRL+C has been pressed.
    /// </summary>
    public class CtrlCRuntimeHostRunAsyncStrategy : IRuntimeHostRunAsyncStrategy, IDisposable
    {
        private readonly CancellationTokenSource cancellationSource = new CancellationTokenSource();
        
        /// <summary>
        /// Finalizes an instance of the <see cref="CtrlCRuntimeHostRunAsyncStrategy"/> class.
        /// </summary>
        ~CtrlCRuntimeHostRunAsyncStrategy()
        {
            Dispose(false);
        }

        /// <inheritdoc />
        public async Task RunAsync(IRuntimeHost host, int startupTimeoutMs, int shutdownTimeoutMs)
        {
            AttachToCancelKeyPressEvent();

            await Console.Out.WriteLineAsync("Press CTRL+C to stop the application...");

            try
            {
                await RunUntilKeyPressed(host, startupTimeoutMs);
            }
            catch (OperationCanceledException)
            {
                // Swallow when cancellation has occurred.
            }
            finally
            {
                await StopTheRuntimeHost(host, shutdownTimeoutMs);

                await Console.Out.WriteLineAsync("Application stopped.");
            }
        }

        private void AttachToCancelKeyPressEvent()
        {
            Console.CancelKeyPress += async (sender, e) =>
            {
                await Console.Out.WriteLineAsync("Stopping application, please be patient as this may take some time...");

                cancellationSource.Cancel();
                e.Cancel = true; // Termination will occur when the host stops running.
            };
        }

        private async Task RunUntilKeyPressed(IRuntimeHost host, int startupTimeoutMs)
        {
            using var startupCancellationSource = new CancellationTokenSource(startupTimeoutMs);

            startupCancellationSource.CancelAfter(startupTimeoutMs);

            await host.StartAsync(startupCancellationSource.Token);
            await Task.Delay(Timeout.InfiniteTimeSpan, cancellationSource.Token);
        }

        private async Task StopTheRuntimeHost(IRuntimeHost host, int shutdownTimeoutMs)
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

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                cancellationSource.Dispose();
            }
        }
    }
}