using AutomationFoundation.Runtime.Threading.Primitives;

namespace AutomationFoundation.Runtime.Abstractions.Threading.Primitives
{
    /// <summary>
    /// Identifies a worker used by the runtime.
    /// </summary>
    public interface IRuntimeWorker : IWorker
    {
        /// <summary>
        /// Initializes the worker.
        /// </summary>
        /// <param name="context">An object containing contextual information for the worker to execute.</param>
        void Initialize(WorkerExecutionContext context);

        /// <summary>
        /// Resets the worker.
        /// </summary>
        void Reset();
    }
}