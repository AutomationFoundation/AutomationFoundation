namespace AutomationFoundation.Hosting.Abstractions;

/// <summary>
/// Identifies the hosting environment.
/// </summary>
public interface IHostingEnvironment
{
    /// <summary>
    /// Determines whether the environment is the environment name specified.
    /// </summary>
    /// <param name="environmentName">The environment name to validate.</param>
    /// <returns>true if the environment name matches the environment, otherwise false.</returns>
    bool IsEnvironment(string environmentName);

    /// <summary>
    /// Gets the environment name.
    /// </summary>
    string EnvironmentName { get; }
}