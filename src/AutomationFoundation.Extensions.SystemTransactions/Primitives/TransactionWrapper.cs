using System;
using System.Transactions;

namespace AutomationFoundation.Extensions.SystemTransactions.Primitives
{
    /// <summary>
    /// Provides a wrapper for a transaction instance. This class is intended for infrastructure only.
    /// </summary>
    /// <typeparam name="TTransaction">The type of transaction to wrap.</typeparam>
    public abstract class TransactionWrapper<TTransaction> : IDisposable
        where TTransaction : Transaction
    {
        /// <summary>
        /// Gets the underlying transaction which has been wrapped.
        /// </summary>
        public TTransaction UnderlyingTransaction { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionWrapper{TTransaction}"/> class.
        /// </summary>
        protected TransactionWrapper()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionWrapper{TTransaction}"/> class.
        /// </summary>
        /// <param name="transaction">The transaction being wrapped.</param>
        protected TransactionWrapper(TTransaction transaction)
        {
            UnderlyingTransaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="TransactionWrapper{TTransaction}"/> class.
        /// </summary>
        ~TransactionWrapper()
        {
            Dispose(false);
        }

        /// <summary>
        /// Rolls the transaction back to the original state.
        /// </summary>
        public virtual void Rollback()
        {
            UnderlyingTransaction.Rollback();
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
                UnderlyingTransaction.Dispose();
            }
        }
    }
}