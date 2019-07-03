using System;
using AutomationFoundation.Builder;
using AutomationFoundation.Runtime.Abstractions;

namespace AutomationFoundation.Hosting.Abstractions.Builder
{
    /// <summary>
    /// Identifies a builder for a runtime.
    /// </summary>
    public interface IRuntimeBuilder : IBuilder<IRuntime>
    {
        /// <summary>
        /// Gets the application services.
        /// </summary>
        IServiceProvider ApplicationServices { get; }

        /// <summary>
        /// Registers a processor with the builder.
        /// </summary>
        /// <param name="processor">The processor to register.</param>
        void RegisterProcessor(Processor processor);
    }
}