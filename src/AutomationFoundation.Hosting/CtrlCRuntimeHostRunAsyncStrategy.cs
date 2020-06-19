using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace AutomationFoundation.Hosting
{
    /// <summary>
    /// Provides a run strategy which runs the host until CTRL+C has been pressed.
    /// </summary>
    public class CtrlCRuntimeHostRunAsyncStrategy : RuntimeHostRunAsyncStrategy
    {
        private readonly ILogger<CtrlCRuntimeHostRunAsyncStrategy> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CtrlCRuntimeHostRunAsyncStrategy"/> class.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        public CtrlCRuntimeHostRunAsyncStrategy(ILogger<CtrlCRuntimeHostRunAsyncStrategy> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc />
        protected override Task AttachToListenForExitAsync()
        {
            Console.CancelKeyPress += (sender, e) =>
            {
                logger.LogInformation("Stopping the application, please be patient as this may take some time...");

                CancellationSource.Cancel();
                e.Cancel = true; // Termination will occur when the host stops running.
            };

            logger.LogInformation("Press CTRL+C to stop the application...");
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        protected override Task OnStoppedAsync()
        {
            logger.LogInformation("Application stopped.");
            return Task.CompletedTask;
        }
    }
}