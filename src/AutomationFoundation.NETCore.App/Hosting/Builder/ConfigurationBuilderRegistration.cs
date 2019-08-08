using System;
using AutomationFoundation.Runtime.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationFoundation.Hosting.Builder
{
    /// <summary>
    /// Contains the extension methods to register for handle building configuration objects..
    /// </summary>
    public static class ConfigurationBuilderRegistration
    {
        /// <summary>
        /// Builds the configuration and attaches it to the services.
        /// </summary>
        /// <param name="services">The collection of services.</param>
        /// <param name="callback">The callback to execute which will contain the application specific logic to execute.</param>
        public static void Build(IServiceCollection services, Action<IConfigurationBuilder> callback)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            else if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            var configurationBuilder = new ConfigurationBuilder();
            callback(configurationBuilder);

            var configuration = configurationBuilder.Build();
            if (configuration == null)
            {
                throw new BuildException("The configuration was not built as expected.");
            }

            services.AddSingleton<IConfiguration>(sp => configuration);
        }
    }
}