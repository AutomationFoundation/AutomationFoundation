using System;
using System.Threading.Tasks;

namespace AutomationFoundation.Runtime.Threading
{
    /// <summary>
    /// Contains extensions for tasks.
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        /// Optionally disposes the task depending on the state of the task.
        /// </summary>
        /// <param name="task">The task to check.</param>
        public static void DisposeIfNecessary(this Task task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            var requiresDisposal = task.IsCanceled || task.IsCompleted || task.IsFaulted;
            if (requiresDisposal)
            {
                task.Dispose();
            }
        }

        /// <summary>
        /// Determines if the task is currently running.
        /// </summary>
        /// <param name="task">The task to check.</param>
        /// <returns>true if the task is currently running, otherwise false.</returns>
        public static bool IsRunning(this Task task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            return task.Status > TaskStatus.WaitingForActivation && task.Status < TaskStatus.RanToCompletion;
        }        
    }
}