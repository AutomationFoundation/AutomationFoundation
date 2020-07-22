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
        /// Runs until signaled to stop by SIGTERM received.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="options">Optional. The options to use.</param>
        /// <returns>The task to await.</returns>
        public static async Task RunUntilSigTermAsync(this IRuntimeHost host, SigTermRuntimeHostRunAsyncOptions options = null)
        {
            using var strategy = new SigTermRuntimeHostRunAsyncStrategy(
                host.ApplicationServices.GetRequiredService<ILogger<SigTermRuntimeHostRunAsyncStrategy>>(),
                options);

            await strategy.RunAsync(host);
        }
    }
}