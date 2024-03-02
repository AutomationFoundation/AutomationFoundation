namespace AutomationFoundation.Hosting.Abstractions.Builders;

/// <summary>
/// Identifies a mechanism which can build hosting environments.
/// </summary>
public interface IHostingEnvironmentBuilder
{
    /// <summary>
    /// Sets the environment name.
    /// </summary>
    /// <param name="environmentName">The name of the environment.</param>
    void SetEnvironmentName(string environmentName);

    /// <summary>
    /// Builds the hosting environment.
    /// </summary>
    /// <returns>The new hosting environment.</returns>
    IHostingEnvironment Build();
}