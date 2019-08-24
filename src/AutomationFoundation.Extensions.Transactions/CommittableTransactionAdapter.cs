using System;
using System.Transactions;
using AutomationFoundation.Extensions.Transactions.Primitives;
using AutomationFoundation.Transactions.Abstractions;

namespace AutomationFoundation.Extensions.Transactions
{
    /// <summary>
    /// Provides an adapter for a <see cref="CommittableTransaction"/> object.
    /// </summary>
    public class CommittableTransactionAdapter : ITransaction<CommittableTransaction>, IDisposable
    {
        private readonly InternalCommittableTransaction transaction;

        /// <inheritdoc />
        public CommittableTransaction UnderlyingTransaction => transaction.UnderlyingTransaction;

        /// <summary>
        /// Gets a value indicating whether the adapter owns the transaction.
        /// </summary>
        public bool OwnsTransaction { get; }

        /// <summary>
        /// Gets a value indicating whether this instance has been disposed.
        /// </summary>
        protected bool IsDisposed { get; private set; }

        /// <summary>
        /// Initializes an instance of the <see cref="CommittableTransactionAdapter"/> class.
        /// </summary>
        /// <param name="transaction">The transaction which is being adapted.</param>
        /// <param name="ownsTransaction">Optional. Identifies whether the adapter will take ownership of the transaction.</param>
        public CommittableTransactionAdapter(CommittableTransaction transaction, bool ownsTransaction = true)
            : this(ownsTransaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            this.transaction = new InternalCommittableTransaction(transaction);
        }

        /// <summary>
        /// Initializes an instance of the <see cref="CommittableTransactionAdapter"/> class.
        /// </summary>
        /// <param name="transaction">The transaction which is being adapted.</param>
        /// <param name="ownsTransaction">Optional. Identifies whether the adapter will take ownership of the transaction.</param>
        internal CommittableTransactionAdapter(InternalCommittableTransaction transaction, bool ownsTransaction = true)
            : this(ownsTransaction)
        {
            this.transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }

        private CommittableTransactionAdapter(bool ownsTransaction)
        {
            OwnsTransaction = ownsTransaction;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="CommittableTransactionAdapter"/> class.
        /// </summary>
        ~CommittableTransactionAdapter()
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
                transaction.Dispose();
            }

            IsDisposed = true;
        }

        /// <inheritdoc />
        public void Commit()
        {
            GuardMustNotBeDisposed();

            transaction.Commit();
        }

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

        /// <inheritdoc />
        public void Rollback()
        {
            GuardMustNotBeDisposed();

            transaction.Rollback();
        }
    }
}