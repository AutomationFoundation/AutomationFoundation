using AutomationFoundation.Runtime.Builders;

namespace AutomationFoundation.Hosting
{
    /// <summary>
    /// Identifies an object which assists with startup of the runtime.
    /// </summary>
    public interface IStartup
    {
        /// <summary>
        /// Configures the processors.
        /// </summary>
        /// <param name="runtimeBuilder">The builder being used to build the runtime.</param>
        /// <param name="environment">The environment in which the application is being hosted.</param>
        void ConfigureProcessors(IRuntimeBuilder runtimeBuilder, IHostingEnvironment environment);
    }
}