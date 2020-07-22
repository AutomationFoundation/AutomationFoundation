using System.Threading.Tasks;
using AutomationFoundation.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AutomationFoundation
{
    /// <summary>
    /// Contains extensions for the runtime host.
    /// </summary>
    public static class RuntimeHostExtensions
    {
        /// <summary>
        /// Runs until signaled to stop by pressing CTRL+C on the console.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="options">Optional. The options to use.</param>
        /// <returns>The task to await.</returns>
        public static async Task RunUntilCtrlCPressedAsync(this IRuntimeHost host, CtrlCRuntimeHostRunAsyncOptions options = null)
        {
            using var strategy = new CtrlCRuntimeHostRunAsyncStrategy(
                host.ApplicationServices.GetRequiredService<ILogger<CtrlCRuntimeHostRunAsyncStrategy>>(),
                options ?? new CtrlCRuntimeHostRunAsyncOptions());

            await strategy.RunAsync(host);
        }
    }
}