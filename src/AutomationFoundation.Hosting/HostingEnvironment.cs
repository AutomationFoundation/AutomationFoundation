using System;

namespace AutomationFoundation.Hosting
{
    /// <summary>
    /// Represents a hosting environment.
    /// </summary>
    public class HostingEnvironment : IHostingEnvironment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HostingEnvironment"/> class.
        /// </summary>
        /// <param name="environmentName">The name of the environment.</param>
        public HostingEnvironment(string environmentName)
        {
            EnvironmentName = environmentName ?? throw new ArgumentNullException(nameof(environmentName));
        }

        /// <inheritdoc />
        public virtual string EnvironmentName { get; }

        /// <inheritdoc />
        public bool IsEnvironment(string environmentName, StringComparison comparisonType = StringComparison.CurrentCultureIgnoreCase)
        {
            if (string.IsNullOrWhiteSpace(environmentName))
            {
                throw new ArgumentNullException(nameof(environmentName));
            }

            return EnvironmentName.StartsWith(environmentName, comparisonType);
        }
    }
}