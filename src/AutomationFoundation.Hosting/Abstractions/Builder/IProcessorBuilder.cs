using AutomationFoundation.Builder;

namespace AutomationFoundation.Hosting.Abstractions.Builder
{
    /// <summary>
    /// Identifies a builder for processors.
    /// </summary>
    /// <typeparam name="TProcessor">The type of processor being built.</typeparam>
    public interface IProcessorBuilder<out TProcessor> : IBuilder<TProcessor>
        where TProcessor : Processor
    {
    }
}