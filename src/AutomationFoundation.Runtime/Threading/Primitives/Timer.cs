using System;
using System.Threading;

namespace AutomationFoundation.Runtime.Threading.Primitives
{
    /// <summary>
    /// Represents a timer.
    /// </summary>
    public class Timer : ITimer
    {
        private readonly object syncRoot = new object();
        private System.Threading.Timer timer;

        private Action onElapsedCallback;
        private Action<Exception> onErrorCallback;
        private TimeSpan interval;

        private bool disposed;

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
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources, otherwise false to release unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                timer?.Dispose();
            }

            disposed = true;
        }

        /// <summary>
        /// Guards against the timer having been disposed.
        /// </summary>
        protected void GuardMustNotBeDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(Timer));
            }
        }
    }
}