using System;
using System.Threading;
using AutomationFoundation.Runtime.Abstractions.Threading.Primitives;

namespace AutomationFoundation.Runtime.Threading.Primitives
{
    /// <summary>
    /// Provides a source for cancellation of operations.
    /// </summary>
    public sealed class CancellationSource : DisposableObject, ICancellationSource
    {
        private readonly CancellationTokenSource cancellationSource;

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

        /// <inheritdoc />
        public bool IsCancellationRequested
        {
            get
            {
                GuardMustNotBeDisposed();
                return cancellationSource.IsCancellationRequested;
            }
        }

        /// <summary>
        /// Gets the cancellation token for the cancellation source.
        /// </summary>
        public CancellationToken CancellationToken
        {
            get
            {
                GuardMustNotBeDisposed();
                return cancellationSource.Token;
            }
        }

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
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                cancellationSource.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}