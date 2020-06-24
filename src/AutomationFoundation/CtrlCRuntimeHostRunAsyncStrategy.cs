using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AutomationFoundation
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
        /// <param name="options">The options.</param>
        public CtrlCRuntimeHostRunAsyncStrategy(ILogger<CtrlCRuntimeHostRunAsyncStrategy> logger, IOptions<CtrlCRuntimeHostRunAsyncOptions> options)
            : base(options)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc />
        protected override Task AttachToListenForExitAsync()
        {
            AttachToCtrlCKeyPressEvent();

            logger.LogInformation("Press CTRL+C to stop the application...");
            return Task.CompletedTask;
        }

        /// <summary>
        /// Attaches the strategy to the key press event.
        /// </summary>
        protected virtual void AttachToCtrlCKeyPressEvent()
        {
            Console.CancelKeyPress += async (sender, e) =>
            {
                e.Cancel = await OnCancelKeyPressed();
            };
        }

        /// <summary>
        /// Occurs when the CTRL+C keys are pressed.
        /// </summary>
        /// <returns>A boolean indicating whether the event should be canceled.</returns>
        protected virtual Task<bool> OnCancelKeyPressed()
        {
            logger.LogInformation("Stopping the application, please be patient as this may take some time...");

            CancellationSource.Cancel();

            return Task.FromResult(true); // Termination will occur when the host stops running.
        }

        /// <inheritdoc />
        protected override Task OnStoppedAsync()
        {
            logger.LogInformation("Application stopped.");
            return Task.CompletedTask;
        }
    }
}