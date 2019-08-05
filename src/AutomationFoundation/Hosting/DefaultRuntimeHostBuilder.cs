using System;
using System.Collections.Generic;
using AutomationFoundation.Hosting.Abstractions;
using AutomationFoundation.Hosting.Abstractions.Builder;
using AutomationFoundation.Runtime.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationFoundation.Hosting
{
    /// <summary>
    /// Provides the default runtime host builder.
    /// </summary>
    public sealed class DefaultRuntimeHostBuilder : IRuntimeHostBuilder
    {
        private readonly IList<Action<IServiceCollection>> configurationCallbacks = new List<Action<IServiceCollection>>();
        private bool startupHasBeenConfigured;

        /// <inheritdoc />
        public IRuntimeHostBuilder ConfigureServices(Action<IServiceCollection> callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            configurationCallbacks.Add(callback);
            return this;
        }

        /// <inheritdoc />
        public IRuntimeHostBuilder UseStartup<TStartup>() where TStartup : IStartup
        {
            configurationCallbacks.Add(services => services.AddScoped(typeof(IStartup), typeof(TStartup)));
            startupHasBeenConfigured = true;

            return this;
        }

        /// <inheritdoc />
        public IRuntimeHostBuilder UseStartup(IStartup startup)
        {
            if (startup == null)
            {
                throw new ArgumentNullException(nameof(startup));
            }

            configurationCallbacks.Add(services => services.AddSingleton(typeof(IStartup), (sp) => startup));
            startupHasBeenConfigured = true;

            return this;
        }

        /// <inheritdoc />
        public IRuntimeHost Build()
        {
            GuardStartupMustBeConfigured();

            var serviceCollection = new ServiceCollection();
            foreach (var configurationCallback in configurationCallbacks)
            {
                configurationCallback(serviceCollection);
            }

            using (var sp = serviceCollection.BuildServiceProvider())
            {
                var startup = sp.GetRequiredService<IStartup>();

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
        }

        private void GuardStartupMustBeConfigured()
        {
            if (!startupHasBeenConfigured)
            {
                throw new BuildException("The startup must be configured.");
            }
        }
    }
}