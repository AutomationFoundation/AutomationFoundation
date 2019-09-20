using System;

namespace AutomationFoundation.Hosting
{
    /// <summary>
    /// Represents the default hosting environment.
    /// </summary>
    public class DefaultHostingEnvironment : HostingEnvironment
    {
        /// <summary>
        /// Defines the name of the environment variable which holds the environment name.
        /// </summary>
        public const string EnvironmentNameVariable = "ASPNETCORE_ENVIRONMENT";

        /// <summary>
        /// Defines the default environment name.
        /// </summary>
        public const string DefaultEnvironmentName = "Development";

        /// <inheritdoc />
        public override string EnvironmentName => GetEnvironmentVariable(EnvironmentNameVariable)
                                                  ?? DefaultEnvironmentName;

        /// <summary>
        /// Retrieves the environment variable from the host machine.
        /// </summary>
        /// <param name="variable">The variable to retrieve.</param>
        /// <returns>The value of the variable, if available.</returns>
        protected virtual string GetEnvironmentVariable(string variable)
        {
            return Environment.GetEnvironmentVariable(variable);
        }
    }
}