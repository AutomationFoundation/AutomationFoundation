using System;
using System.Threading;
using AutomationFoundation.Runtime.Abstractions.Threading.Primitives;

namespace AutomationFoundation.Runtime.Threading.Primitives
{
    /// <summary>
    /// Provides a source for cancellation of operations.
    /// </summary>
    public sealed class CancellationSource : ICancellationSource, IDisposable
    {
        private readonly CancellationTokenSource cancellationSource;
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="CancellationSource"/> class.
        /// </summary>
        public CancellationSource()
        {
            cancellationSource = new CancellationTokenSource();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CancellationSource"/> class.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token which will be linked to this cancellation source.</param>
        public CancellationSource(CancellationToken cancellationToken)
        {
            cancellationSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="cancellationSource"/> class.
        /// </summary>
        ~CancellationSource()
        {
            Dispose(false);
        }

        /// <inheritdoc />
        public bool IsCancellationRequested => cancellationSource.IsCancellationRequested;

        /// <summary>
        /// Gets the cancellation token for the cancellation source.
        /// </summary>
        public CancellationToken CancellationToken => cancellationSource.Token;

        /// <inheritdoc />
        public void RequestImmediateCancellation()
        {
            GuardMustNotBeDisposed();

            cancellationSource.Cancel();
        }

        /// <inheritdoc />
        public void RequestCancellationAfter(TimeSpan delay)
        {
            if (delay < TimeSpan.Zero)
            {
                throw new ArgumentException("The value must be greater than or equal to zero.", nameof(delay));
            }

            GuardMustNotBeDisposed();

            cancellationSource.CancelAfter(delay);
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
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                cancellationSource.Dispose();
            }

            disposed = true;
        }


        /// <summary>
        /// Guards against the policy having been disposed.
        /// </summary>
        private void GuardMustNotBeDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(CancellationSource));
            }
        }
    }
}