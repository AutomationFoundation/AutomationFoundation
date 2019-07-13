using System;

namespace AutomationFoundation.Runtime
{
    /// <summary>
    /// Identifies an object which provides contextual information for an item being processed by the runtime.
    /// </summary>
    public interface IProcessingContext
    {
        /// <summary>
        /// Gets the identifier of the item being processed.
        /// </summary>
        Guid Id { get; }
    }
}