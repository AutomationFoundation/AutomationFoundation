using System;

namespace AutomationFoundation.Runtime.Abstractions.Threading.Primitives
{
    /// <summary>
    /// Identifies a timer.
    /// </summary>
    public interface ITimer : IDisposable
    {
        /// <summary>
        /// Starts the timer.
        /// </summary>
        /// <param name="interval">The interval which to invoke the callback.</param>
        /// <param name="onElapsedCallback">The callback to invoke when the timer elapses.</param>
        /// <param name="onErrorCallback">The callback to invoke when an error occurs.</param>
        void Start(TimeSpan interval, Action onElapsedCallback, Action<Exception> onErrorCallback);

        /// <summary>
        /// Stops the timer.
        /// </summary>
        void Stop();
    }
}