using System.Threading.Tasks;
using AutomationFoundation.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationFoundation
{
    /// <summary>
    /// Contains extensions for the runtime host.
    /// </summary>
    public static class RuntimeHostExtensions
    {
        /// <summary>
        /// Runs until signaled to stop via the run strategy.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <returns>The task to await.</returns>
        public static async Task RunAsync(this IRuntimeHost host)
        {
            var runStrategy = host.ApplicationServices.GetService<IRuntimeHostRunAsyncStrategy>();
            if (runStrategy == null)
            {
                throw new HostingException($"The run strategy has not been defined. Please ensure the {nameof(IRuntimeHostBuilder.UseRunStrategy)} method has been called on the {nameof(IRuntimeHostBuilder)}.");
            }

            await runStrategy.RunAsync(host);
        }
    }
}