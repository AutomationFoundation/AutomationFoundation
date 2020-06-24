using Microsoft.Extensions.DependencyInjection;

namespace AutomationFoundation.Hosting
{
    /// <summary>
    /// Identifies an object which configures the services for a service collection.
    /// </summary>
    public interface IConfigureServices
    {
        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services collection which shall receive the new services.</param>
        void ConfigureServices(IServiceCollection services);
    }
}