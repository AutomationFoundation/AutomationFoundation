using AutomationFoundation.Runtime.Abstractions;

namespace AutomationFoundation.Hosting.Abstractions.Builder
{
    /// <summary>
    /// Identifies a builder for processors.
    /// </summary>
    public interface IProcessorBuilder
    {
        /// <summary>
        /// Builds the processor.
        /// </summary>
        /// <returns>The new processor which was built.</returns>
        IProcessor Build();
    }
}