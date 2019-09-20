using System;
using AutomationFoundation.Hosting.Abstractions;

namespace AutomationFoundation.Hosting
{
    /// <summary>
    /// Represents a hosting environment. This class must be inherited.
    /// </summary>
    public abstract class HostingEnvironment : IHostingEnvironment
    {
        /// <inheritdoc />
        public abstract string EnvironmentName { get; }

        /// <inheritdoc />
        public bool IsEnvironment(string environmentName)
        {
            if (string.IsNullOrWhiteSpace(environmentName))
            {
                throw new ArgumentNullException(nameof(environmentName));
            }

            return EnvironmentName.StartsWith(environmentName, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}