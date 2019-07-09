using System;
using System.Collections.Generic;
using AutomationFoundation.Hosting;
using AutomationFoundation.Hosting.Abstractions;
using AutomationFoundation.Hosting.Abstractions.Builder;
using AutomationFoundation.Runtime.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationFoundation
{
    public class DefaultRuntimeHostBuilder : IRuntimeHostBuilder
    {
        private readonly IList<Action<IServiceCollection>> configurationCallbacks = new List<Action<IServiceCollection>>();
        private IStartup startup;

        public IRuntimeHostBuilder ConfigureServices(Action<IServiceCollection> callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            configurationCallbacks.Add(callback);
            return this;
        }

        public IRuntimeHostBuilder UseStartup<TStartup>() where TStartup : IStartup, new()
        {
            startup = new TStartup();
            return this;
        }

        public IRuntimeHost Build()
        {
            GuardStartupMustBeConfigured();

            var serviceCollection = new ServiceCollection();
            foreach (var configurationCallback in configurationCallbacks)
            {
                configurationCallback(serviceCollection);
            }

            var applicationServices = startup.TryConfigureContainer(serviceCollection);
            if (applicationServices == null)
            {
                throw new InvalidOperationException("The services was not configured.");
            }

            var runtimeBuilder = new DefaultRuntimeBuilder(applicationServices);
            startup.ConfigureProcessors(runtimeBuilder);

            var runtime = runtimeBuilder.Build();
            if (runtime == null)
            {
                throw new BuildException("The runtime could not be built.");
            }

            return new RuntimeHost(
                runtime,
                applicationServices);
        }

        private void GuardStartupMustBeConfigured()
        {
            if (startup == null)
            {
                throw new BuildException("The startup class must be configured.");
            }
        }
    }
}