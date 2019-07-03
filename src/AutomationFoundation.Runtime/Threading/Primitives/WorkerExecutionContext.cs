using System;

namespace AutomationFoundation.Runtime.Threading.Primitives
{
    /// <summary>
    /// Provides contextual data for a worker execution.
    /// </summary>
    public struct WorkerExecutionContext
    {
        /// <summary>
        /// Gets or sets the callback to execute on run.
        /// </summary>
        public Action OnRunCallback { get; set; }

        /// <summary>
        /// Gets or sets the callback to run after the execution has completed.
        /// </summary>
        public Action PostCompletedCallback { get; set; }
    }
}