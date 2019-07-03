using System;
using System.Threading;
using AutomationFoundation.Runtime.Abstractions.Threading.Primitives;

namespace AutomationFoundation.Runtime.Threading.Primitives
{
    /// <summary>
    /// Represents a timer.
    /// </summary>
    public class Timer : DisposableObject, ITimer
    {
        private readonly object syncRoot = new object();
        private System.Threading.Timer timer;

        private Action onElapsedCallback;
        private Action<Exception> onErrorCallback;
        private TimeSpan interval;

        /// <summary>
        /// Initializes a new instance of the <see cref="Timer"/> class.
        /// </summary>
        public Timer()
        {
        }

        /// <inheritdoc />
        public void Start(TimeSpan interval, Action onElapsedCallback, Action<Exception> onErrorCallback)
        {
            if (interval <= TimeSpan.Zero)
            {
                throw new ArgumentException("The interval must be greater than zero.", nameof(interval));
            }
            else if (onElapsedCallback == null)
            {
                throw new ArgumentNullException(nameof(onElapsedCallback));
            }
            else if (onErrorCallback == null)
            {
                throw new ArgumentNullException(nameof(onErrorCallback));
            }

            GuardMustNotBeDisposed();

            lock (syncRoot)
            {
                this.interval = interval;
                this.onElapsedCallback = onElapsedCallback;
                this.onErrorCallback = onErrorCallback;

                timer = new System.Threading.Timer(OnRunCallback, null, interval, Timeout.InfiniteTimeSpan);
            }
        }

        private void OnRunCallback(object state)
        {
            try
            {
                onElapsedCallback();
            }
            catch (Exception ex)
            {
                onErrorCallback(ex);
            }
            finally
            {
                timer.Change(interval, Timeout.InfiniteTimeSpan);
            }
        }

        /// <inheritdoc />
        public void Stop()
        {
            GuardMustNotBeDisposed();
            
            lock (syncRoot)
            {
                timer.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
            }
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                timer?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}