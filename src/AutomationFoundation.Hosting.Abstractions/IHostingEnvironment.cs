using System;

namespace AutomationFoundation.Hosting
{
    /// <summary>
    /// Identifies the hosting environment.
    /// </summary>
    public interface IHostingEnvironment
    {
        /// <summary>
        /// Determines whether the environment is the environment name specified.
        /// </summary>
        /// <param name="environmentName">The environment name to validate.</param>
        /// <param name="comparisonType">Optional. The type of comparison to make on the environment name.</param>
        /// <returns>true if the environment name matches the environment, otherwise false.</returns>
        bool IsEnvironment(string environmentName, StringComparison comparisonType = StringComparison.CurrentCultureIgnoreCase);

        /// <summary>
        /// Gets the environment name.
        /// </summary>
        string EnvironmentName { get; }
    }
}