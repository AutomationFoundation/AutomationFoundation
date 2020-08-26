using System;
using AutomationFoundation.Hosting;
using AutomationFoundation.Runtime;

namespace AutomationFoundation
{
    internal class DefaultRuntimeHostBuilder : RuntimeHostBuilder
    {
        /// <inheritdoc />
        protected override IRuntimeHost CreateRuntimeHost(IRuntime runtime, IHostingEnvironment environment, IServiceProvider applicationServices)
        {
            return new RuntimeHost(runtime, 
                environment, 
                applicationServices);
        }
    }
}