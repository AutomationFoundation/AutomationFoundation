using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AutomationFoundation.Hosting
{
    /// <summary>
    /// Provides a run strategy which runs the host until a SIGTERM event has been received.
    /// </summary>
    internal class SigTermRuntimeHostRunAsyncStrategy : RuntimeHostRunAsyncStrategy
    {
        private readonly ManualResetEventSlim waitHandle = new ManualResetEventSlim(false);
        private readonly ILogger<SigTermRuntimeHostRunAsyncStrategy> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SigTermRuntimeHostRunAsyncStrategy"/> class.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The options.</param>
        public SigTermRuntimeHostRunAsyncStrategy(ILogger<SigTermRuntimeHostRunAsyncStrategy> logger, IOptions<SigTermRuntimeHostRunAsyncOptions> options)
            : this(logger, options?.Value)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SigTermRuntimeHostRunAsyncStrategy"/> class.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The options.</param>
        public SigTermRuntimeHostRunAsyncStrategy(ILogger<SigTermRuntimeHostRunAsyncStrategy> logger, SigTermRuntimeHostRunAsyncOptions options)
            : base(options)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc />
        protected override Task AttachToListenForExitAsync()
        {
            AttachToProcessExitEvent();

            logger.LogInformation("Waiting for SIGTERM to stop the application...");
            return Task.CompletedTask;
        }

        /// <summary>
        /// Attaches the strategy to the process exit event.
        /// </summary>
        protected virtual void AttachToProcessExitEvent()
        {
            AppDomain.CurrentDomain.ProcessExit += async (sender, e) =>
            {
                await OnProcessExitedAsync();

                // Intentionally block the event handler from completing, this will allow the application to terminate normally.
                waitHandle.Wait();
            };
        }

        /// <summary>
        /// Occurs when the ProcessExit event has been raised.
        /// </summary>
        /// <returns>The task to await.</returns>
        protected virtual Task OnProcessExitedAsync()
        {
            logger.LogInformation("SIGTERM received, attempting to stop the application...");

            CancellationSource.Cancel();

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