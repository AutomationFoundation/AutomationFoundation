using Microsoft.Extensions.DependencyInjection;

namespace AutomationFoundation.Hosting
{
    /// <summary>
    /// Identifies an object which configures a container.
    /// </summary>
    /// <typeparam name="TBuilder">The type of builder for the container.</typeparam>
    /// <remarks>This interface requires an <see cref="IServiceProviderFactory{TContainerBuilder}"/> to be registered with the container.</remarks>
    public interface IConfigureContainer<in TBuilder>
    {
        /// <summary>
        /// Configures the container.
        /// </summary>
        /// <param name="builder">The builder which is building the container.</param>
        void ConfigureContainer(TBuilder builder);
    }
}