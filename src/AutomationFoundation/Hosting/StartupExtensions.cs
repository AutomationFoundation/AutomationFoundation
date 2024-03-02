using System;
using System.Linq;
using AutomationFoundation.Hosting.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationFoundation.Hosting;

internal static class StartupExtensions
{
    public static IServiceProvider TryConfigureContainer(this IStartup startupInstance, IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        var startupType = startupInstance.GetType();
        var serviceProvider = services.BuildServiceProvider();

        var serviceConfigurator = startupInstance as IConfigureServices;
        serviceConfigurator?.ConfigureServices(services);

        var configureMethod = startupType.GetMethod(nameof(IConfigureContainer<object>.ConfigureContainer));
        if (configureMethod != null)
        {
            var containerBuilderType = configureMethod.GetParameters().Select(o => o.ParameterType).SingleOrDefault();
            var providerFactoryType = typeof(IServiceProviderFactory<>).MakeGenericType(containerBuilderType);

            var configureContainerType = typeof(IConfigureContainer<>).MakeGenericType(containerBuilderType);
            if (configureContainerType.IsAssignableFrom(startupType))
            {
                var createBuilderMethod = providerFactoryType.GetMethod(nameof(IServiceProviderFactory<object>.CreateBuilder));
                if (createBuilderMethod == null)
                {
                    throw new InvalidOperationException($"The method '{nameof(IServiceProviderFactory<object>.CreateBuilder)}' was not found on the builder factory.");
                }

                var providerFactory = serviceProvider.GetService(providerFactoryType);
                if (providerFactory == null)
                {
                    throw new InvalidOperationException("The provider factory could not be located on the container. Ensure the service provider factory has been registered.");
                }

                var builder = createBuilderMethod.Invoke(providerFactory, new object[] { services });
                configureMethod.Invoke(startupInstance, new[] { builder });

                var createProviderMethod = providerFactoryType.GetMethod(nameof(IServiceProviderFactory<object>.CreateServiceProvider));
                if (createProviderMethod == null)
                {
                    throw new InvalidOperationException($"The method '{nameof(IServiceProviderFactory<object>.CreateServiceProvider)}' was not found on the provider factory.");
                }

                return (IServiceProvider)createProviderMethod.Invoke(providerFactory, new[] { builder });
            }
        }

        return services.BuildServiceProvider();
    }
}