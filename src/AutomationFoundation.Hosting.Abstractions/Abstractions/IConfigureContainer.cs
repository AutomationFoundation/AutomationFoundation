namespace AutomationFoundation.Hosting.Abstractions;

/// <summary>
/// Identifies an object which configures a container.
/// </summary>
/// <typeparam name="TBuilder">The type of builder for the container.</typeparam>
public interface IConfigureContainer<in TBuilder>
{
    /// <summary>
    /// Configures the container.
    /// </summary>
    /// <param name="builder">The builder which is building the container.</param>
    void ConfigureContainer(TBuilder builder);
}