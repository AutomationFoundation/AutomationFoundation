using System;

namespace AutomationFoundation.Transactions.Abstractions
{
    /// <summary>
    /// Provides a base implementation of a transaction adapter.
    /// </summary>
    /// <typeparam name="TTransaction">The type of the underlying transaction.</typeparam>
    public abstract class TransactionAdapter<TTransaction> : ITransaction<TTransaction>, IDisposable
    {
        /// <inheritdoc />
        public abstract TTransaction UnderlyingTransaction { get; }

        /// <inheritdoc />
        public abstract void Rollback();

        /// <inheritdoc />
        public abstract void Commit();

        /// <summary>
        /// Gets a value indicating whether the adapter owns the transaction.
        /// </summary>
        public bool OwnsTransaction { get; }

        /// <summary>
        /// Gets a value indicating whether this instance has been disposed.
        /// </summary>
        protected bool IsDisposed { get; private set; }

        /// <summary>
        /// Initializes an instance of the <see cref="TransactionAdapter{TTransaction}"/> class.
        /// </summary>
        /// <param name="ownsTransaction">Optional. Identifies whether the adapter will take ownership of the transaction.</param>
        protected TransactionAdapter(bool ownsTransaction)
        {
            OwnsTransaction = ownsTransaction;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="TransactionAdapter{TTransaction}"/> class.
        /// </summary>
        ~TransactionAdapter()
        {
            Dispose(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
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
            if (disposing && OwnsTransaction)
            {
                DisposeUnderlyingTransaction();
            }

            IsDisposed = true;
        }

        /// <summary>
        /// Disposes the underlying transaction.
        /// </summary>
        protected abstract void DisposeUnderlyingTransaction();

        /// <summary>
        /// Guards the object to ensure it has not been previously disposed.
        /// </summary>
        protected void GuardMustNotBeDisposed()
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException("The object has been disposed.");
            }
        }
    }
}