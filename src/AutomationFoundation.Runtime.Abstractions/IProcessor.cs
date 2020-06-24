using System;

namespace AutomationFoundation.Runtime
{
    /// <summary>
    /// Identifies a processor.
    /// </summary>
    public interface IProcessor : IStartable, IStoppable, IDisposable
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the processor state.
        /// </summary>
        ProcessorState State { get; }
    }
}