using AutomationFoundation.Hosting.Abstractions;

namespace AutomationFoundation.Hosting;

/// <summary>
/// Contains extension methods for the hosting environment.
/// </summary>
public static class HostingEnvironmentExtensions
{
    /// <summary>
    /// Determines whether the application is being hosted in a production environment.
    /// </summary>
    /// <param name="environment">The environment to check.</param>
    /// <returns>true if the environment is production, otherwise false.</returns>
    public static bool IsProduction(this IHostingEnvironment environment)
    {
        return environment.IsEnvironment("Prod");
    }

    /// <summary>
    /// Determines whether the application is being hosted in a non-production environment.
    /// </summary>
    /// <param name="environment">The environment to check.</param>
    /// <returns>true if the environment is non-production, otherwise false.</returns>
    public static bool IsNonProduction(this IHostingEnvironment environment)
    {
        return !environment.IsProduction();
    }

    /// <summary>
    /// Determines whether the application is being hosted in a development environment.
    /// </summary>
    /// <param name="environment">The environment to check.</param>
    /// <returns>true if the environment is development, otherwise false.</returns>
    public static bool IsDevelopment(this IHostingEnvironment environment)
    {
        return environment.IsEnvironment("Dev");
    }
}