using AutomationFoundation.Runtime.Abstractions.Builders;

namespace AutomationFoundation.Hosting.Abstractions
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
        void ConfigureProcessors(IRuntimeBuilder runtimeBuilder);
    }
}