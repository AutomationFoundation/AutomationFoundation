using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutomationFoundation.Runtime.Threading
{
    /// <summary>
    /// Contains extensions for tasks.
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        /// Abandons the task when the cancellation token is signaled.
        /// </summary>
        /// <param name="task">The task which should be abandoned.</param>
        /// <param name="cancellationToken">The cancellation token to monitor for cancellation requests.</param>
        /// <returns>The task to await.</returns>
        /// <exception cref="TaskAbandonedException">The task was abandoned.</exception>
        public static async Task AbandonWhen(this Task task, CancellationToken cancellationToken)
        {
            var completedTask = await Task.WhenAny(task, Task.Delay(Timeout.Infinite, cancellationToken));
            if (completedTask == task)
            {
                await task; // Required to propagate exceptions (if any occurred).
                return;
            }

            throw new TaskAbandonedException();
        }

        /// <summary>
        /// Abandons the task when the cancellation token is signaled.
        /// </summary>
        /// <param name="task">The task which should be abandoned.</param>
        /// <param name="cancellationToken">The cancellation token to monitor for cancellation requests.</param>
        /// <returns>The task to await.</returns>
        public static async Task<TResult> AbandonWhen<TResult>(this Task<TResult> task, CancellationToken cancellationToken)
        {
            var completedTask = await Task.WhenAny(task, Task.Delay(Timeout.Infinite, cancellationToken));
            if (completedTask == task)
            {
                return await task; // Required to propagate exceptions (if any occurred).
            }

            throw new TaskAbandonedException();
        }

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