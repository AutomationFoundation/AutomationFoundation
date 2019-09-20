using System;
using AutomationFoundation.Runtime.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationFoundation.Hosting.Builders
{
    /// <summary>
    /// Provides a registration for performing configuration builder tasks during startup.
    /// </summary>
    public class ConfigurationBuilderRegistration
    {
        private readonly IConfigurationBuilder builder;
        private readonly IServiceCollection services;
        private readonly Action<IConfigurationBuilder> callback;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationBuilderRegistration"/> class.
        /// </summary>
        /// <param name="builder">The builder to use when building the configuration.</param>
        /// <param name="services">The collection of services.</param>
        /// <param name="callback">The callback to execute which will contain the application specific logic to execute.</param>
        public ConfigurationBuilderRegistration(IConfigurationBuilder builder, IServiceCollection services, Action<IConfigurationBuilder> callback)
        {
            this.builder = builder ?? throw new ArgumentNullException(nameof(builder));
            this.services = services ?? throw new ArgumentNullException(nameof(services));
            this.callback = callback ?? throw new ArgumentNullException(nameof(callback));
        }

        /// <summary>
        /// Registers the object.
        /// </summary>
        public void Register()
        {
            callback(builder);

            var configuration = builder.Build();
            if (configuration == null)
            {
                throw new BuildException("The configuration was not built as expected.");
            }

            services.AddSingleton<IConfiguration>(sp => configuration);
        }

        /// <summary>
        /// Callback which is executed when the service collection is being built.
        /// </summary>
        /// <param name="services">The collection of services.</param>
        /// <param name="callback">The callback to execute which will contain the application specific logic to execute.</param>
        public static void OnConfigureServicesCallback(IServiceCollection services, Action<IConfigurationBuilder> callback)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            else if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            new ConfigurationBuilderRegistration(
                new ConfigurationBuilder(),
                services,
                callback).Register();
        }
    }
}