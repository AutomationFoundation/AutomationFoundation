using System;
using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Interop;
using AutomationFoundation.Interop.Primitives;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using static AutomationFoundation.Interop.ConsoleApi;

namespace AutomationFoundation
{
    /// <summary>
    /// Provides a run strategy which runs the host until a TASKKILL event has been received.
    /// </summary>
    public class TaskKillRuntimeHostRunAsyncStrategy : RuntimeHostRunAsyncStrategy
    {
        private readonly IKernel32 kernel32 = new Kernel32();
        private readonly ManualResetEventSlim waitHandle = new ManualResetEventSlim(false);

        private readonly ILogger<TaskKillRuntimeHostRunAsyncStrategy> logger;
        private readonly HandlerRoutine callback;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskKillRuntimeHostRunAsyncStrategy"/> class.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The options.</param>
        public TaskKillRuntimeHostRunAsyncStrategy(ILogger<TaskKillRuntimeHostRunAsyncStrategy> logger, IOptions<TaskKillRuntimeHostRunAsyncOptions> options) 
            : base(options)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            callback = OnCtrlMessageReceived;
        }

        /// <inheritdoc />
        protected override Task AttachToListenForExitAsync()
        {
            kernel32.SetConsoleCtrlHandler(callback, true);

            logger.LogInformation("Waiting for TASKKILL to stop the application...");
            return Task.CompletedTask;
        }

        private bool OnCtrlMessageReceived(int dwCtrlType)
        {
            if (!ShouldTerminateApplication(dwCtrlType))
            {
                return true;
            }

            logger.LogInformation("TASKKILL received, attempting to stop the application...");

            CancellationSource.Cancel();
            waitHandle.Wait(); // Intentionally block the event handler from completing, this will allow the application to terminate normally.

            return true;
        }

        /// <summary>
        /// Determines whether the application should be terminated.
        /// </summary>
        /// <param name="dwCtrlType">The control type passed from the operating system.</param>
        /// <returns>true if the application should be terminated, otherwise false.</returns>
        protected virtual bool ShouldTerminateApplication(int dwCtrlType)
        {
            return dwCtrlType == CTRL_CLOSE_EVENT || dwCtrlType == CTRL_LOGOFF_EVENT;
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