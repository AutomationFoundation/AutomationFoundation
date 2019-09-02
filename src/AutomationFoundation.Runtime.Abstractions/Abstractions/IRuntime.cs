using System;

namespace AutomationFoundation.Runtime.Abstractions
{
    /// <summary>
    /// Identifies a runtime.
    /// </summary>
    public interface IRuntime : IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether the runtime any processors which are started.
        /// </summary>
        bool IsActive { get; }

        /// <summary>
        /// Starts the runtime.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops the runtime.
        /// </summary>
        void Stop();
    }
}