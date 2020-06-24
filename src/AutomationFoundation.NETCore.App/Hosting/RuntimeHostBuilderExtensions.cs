using System;
using AutomationFoundation.Hosting.Registrations;
using Microsoft.Extensions.Configuration;

namespace AutomationFoundation.Hosting
{
    /// <summary>
    /// Contains extension methods for the runtime host builder.
    /// </summary>
    public static class RuntimeHostBuilderExtensions
    {
        /// <summary>
        /// Configures the application configuration.
        /// </summary>
        /// <param name="builder">The builder instance.</param>
        /// <param name="callback">The callback which will configure the application configuration.</param>
        /// <returns>The builder instance.</returns>
        public static IRuntimeHostBuilder ConfigureAppConfiguration(this IRuntimeHostBuilder builder, Action<IConfigurationBuilder> callback)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            else if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            builder.ConfigureServices(services => 
                ConfigurationBuilderRegistration.OnConfigureServicesCallback(services, callback));

            return builder;
        }
    }
}