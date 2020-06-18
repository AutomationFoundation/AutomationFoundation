namespace AutomationFoundation.Hosting.Abstractions.Builders
{
    /// <summary>
    /// Identifies a mechanism which can build hosting environments.
    /// </summary>
    public interface IHostingEnvironmentBuilder
    {
        /// <summary>
        /// Sets the environment name.
        /// </summary>
        /// <param name="value">The name of the environment.</param>
        void SetEnvironmentName(string value);

        /// <summary>
        /// Builds the hosting environment.
        /// </summary>
        /// <returns>The new hosting environment.</returns>
        IHostingEnvironment Build();
    }
}