using System;
using AutomationFoundation.Hosting.Abstractions;
using AutomationFoundation.Hosting.Abstractions.Builders;

namespace AutomationFoundation.Hosting
{
    /// <summary>
    /// Provides a default builder for hosting environments.
    /// </summary>
    public class DefaultHostingEnvironmentBuilder : IHostingEnvironmentBuilder
    {
        /// <summary>
        /// Defines the name of the environment variable which holds the environment name.
        /// </summary>
        private const string EnvironmentNameVariable = "ASPNETCORE_ENVIRONMENT";

        /// <summary>
        /// Defines the default environment name.
        /// </summary>
        private const string DefaultEnvironmentName = "Development";

        private string environmentName;

        /// <inheritdoc />
        public void SetEnvironmentName(string environmentName)
        {
            if (string.IsNullOrWhiteSpace(environmentName))
            {
                throw new ArgumentNullException(nameof(environmentName));
            }

            this.environmentName = environmentName;
        }

        /// <inheritdoc />
        public IHostingEnvironment Build()
        {
            var name = DetermineEnvironmentName();

            return new HostingEnvironment(name);
        }

        private string DetermineEnvironmentName()
        {
            if (!string.IsNullOrWhiteSpace(environmentName))
            {
                return environmentName;
            }

            var result = GetEnvironmentVariable(EnvironmentNameVariable);
            if (!string.IsNullOrWhiteSpace(result))
            {
                return result;
            }

            return DefaultEnvironmentName;
        }

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