using System.Threading.Tasks;

namespace AutomationFoundation.Runtime.Abstractions
{
    /// <summary>
    /// Identifies an object which can be started.
    /// </summary>
    public interface IStartable
    {
        /// <summary>
        /// Starts the object asynchronously.
        /// </summary>
        /// <returns>The asynchronous task.</returns>
        Task StartAsync();
    }

    /// <summary>
    /// Identifies an object which can be started.
    /// </summary>
    /// <typeparam name="TContext">The type of object which contains contextual information while the object is being started.</typeparam>
    public interface IStartable<in TContext>
    {
        /// <summary>
        /// Starts the object asynchronously.
        /// </summary>
        /// <param name="context">The contextual information.</param>
        /// <returns>The asynchronous task.</returns>
        Task StartAsync(TContext context);
    }
}