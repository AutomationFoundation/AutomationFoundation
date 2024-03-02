using System;

namespace AutomationFoundation.Runtime.Abstractions.Builders;

/// <summary>
/// Identifies a builder for a runtime.
/// </summary>
public interface IRuntimeBuilder
{
    /// <summary>
    /// Gets the application services.
    /// </summary>
    IServiceProvider ApplicationServices { get; }

    /// <summary>
    /// Registers a processor with the builder.
    /// </summary>
    /// <param name="processor">The processor to register.</param>
    IRuntimeBuilder RegisterProcessor(IProcessor processor);

    /// <summary>
    /// Builds the runtime.
    /// </summary>
    /// <returns>The new runtime which was built.</returns>
    IRuntime Build();
}