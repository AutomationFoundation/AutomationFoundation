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
        /// Runs until signaled to stop by TASKKILL received.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="options">Optional. The options to use.</param>
        /// <returns>The task to await.</returns>
        public static async Task RunUntilTaskKillAsync(this IRuntimeHost host, TaskKillRuntimeHostRunAsyncOptions options = null)
        {
            using var strategy = new TaskKillRuntimeHostRunAsyncStrategy(
                host.ApplicationServices.GetRequiredService<ILogger<TaskKillRuntimeHostRunAsyncStrategy>>(),
                options ?? new TaskKillRuntimeHostRunAsyncOptions());

            await strategy.RunAsync(host);
        }
    }
}