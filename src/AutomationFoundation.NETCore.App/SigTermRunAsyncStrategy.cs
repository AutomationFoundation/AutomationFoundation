using System;
using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Hosting;
using Microsoft.Extensions.Logging;

namespace AutomationFoundation
{
    /// <summary>
    /// Provides a run strategy which runs the host until a SIGTERM event has been received.
    /// </summary>
    public class SigTermRunAsyncStrategy : RuntimeHostRunAsyncStrategy
    {
        private readonly ManualResetEventSlim waitHandle = new ManualResetEventSlim(false);
        private readonly ILogger<SigTermRunAsyncStrategy> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SigTermRunAsyncStrategy"/> class.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        public SigTermRunAsyncStrategy(ILogger<SigTermRunAsyncStrategy> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc />
        protected override Task AttachToListenForExitAsync()
        {
            AppDomain.CurrentDomain.ProcessExit += (sender, e) =>
            {
                logger.LogInformation("SIGTERM received, attempting to stop the application...");

                CancellationSource.Cancel();
                waitHandle.Wait(); // Intentionally block the event handler from completing, this will allow the application to terminate normally.
            };

            logger.LogInformation("Waiting for SIGTERM to stop the application...");
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        protected override Task OnStoppedAsync()
        {
            logger.LogInformation("Application stopped.");
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                waitHandle.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}