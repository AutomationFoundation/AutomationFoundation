using System;
using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Interop;
using AutomationFoundation.Interop.Primitives;
using Microsoft.Extensions.Logging;
using static AutomationFoundation.Interop.ConsoleApi;
using static AutomationFoundation.Interop.NativeMethods;

namespace AutomationFoundation
{
    /// <summary>
    /// Provides a run strategy which runs the host until a TASKKILL event has been received.
    /// </summary>
    internal class TaskKillRuntimeHostRunAsyncStrategy : RuntimeHostRunAsyncStrategy
    {
        private readonly ManualResetEventSlim waitHandle = new ManualResetEventSlim(false);
        
        private readonly IKernel32 kernel32;
        private readonly ILogger<TaskKillRuntimeHostRunAsyncStrategy> logger;
        private readonly HandlerRoutine callback;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskKillRuntimeHostRunAsyncStrategy"/> class.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The options.</param>
        public TaskKillRuntimeHostRunAsyncStrategy(ILogger<TaskKillRuntimeHostRunAsyncStrategy> logger, TaskKillRuntimeHostRunAsyncOptions options) 
            : this(new Kernel32(), logger, options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskKillRuntimeHostRunAsyncStrategy"/> class.
        /// </summary>
        /// <param name="kernel32">The kernel instance.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="options">The options.</param>
        public TaskKillRuntimeHostRunAsyncStrategy(IKernel32 kernel32, ILogger<TaskKillRuntimeHostRunAsyncStrategy> logger, TaskKillRuntimeHostRunAsyncOptions options) 
            : base(options)
        {
            this.kernel32 = kernel32 ?? throw new ArgumentNullException(nameof(kernel32));
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
        
        /// <summary>
        /// Occurs when the CTRL message is received.
        /// </summary>
        /// <param name="dwCtrlType">The control type.</param>
        /// <returns>true if the event has been handled, otherwise false to allow other handlers to accept the control message.</returns>
        protected bool OnCtrlMessageReceived(int dwCtrlType)
        {
            if (!ShouldTerminateApplication(dwCtrlType))
            {
                // Ensures other handlers cannot terminate the console application.
                return true;
            }

            logger.LogInformation("TASKKILL received, attempting to stop the application...");

            CancellationSource.Cancel();
            WaitUntilTerminated();

            return true;
        }

        /// <summary>
        /// BLOCKING: This will block the process from going any further until the application has completed normally.
        /// </summary>
        protected virtual void WaitUntilTerminated()
        {
            // Intentionally block the event handler from completing, this will allow the application to terminate normally.
            waitHandle.Wait();
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