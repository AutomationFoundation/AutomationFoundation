using System;
using System.Data.Common;
using AutomationFoundation.Transactions.Abstractions;

namespace AutomationFoundation.Extensions.SystemData
{
    /// <summary>
    /// Provides an adapter for a <see cref="DbTransaction"/> transaction.
    /// </summary>
    /// <typeparam name="TTransaction">The type of transaction being adapted.</typeparam>
    public sealed class DbTransactionAdapter<TTransaction> : TransactionAdapter<TTransaction>
        where TTransaction : DbTransaction
    {
        /// <summary>
        /// Initializes an instance of the <see cref="DbTransactionAdapter{TTransaction}"/> class.
        /// </summary>
        /// <param name="transaction">The transaction which is being adapted.</param>
        /// <param name="ownsTransaction">Optional. Identifies whether the adapter will take ownership of the transaction.</param>
        public DbTransactionAdapter(TTransaction transaction, bool ownsTransaction = true) 
            : base(ownsTransaction)
        {
            UnderlyingTransaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }

        /// <inheritdoc />
        public override TTransaction UnderlyingTransaction { get; }

        /// <inheritdoc />
        public override void Rollback()
        {
            GuardMustNotBeDisposed();

            UnderlyingTransaction.Rollback();
        }

        /// <inheritdoc />
        public override void Commit()
        {
            GuardMustNotBeDisposed();

            UnderlyingTransaction.Commit();
        }

        /// <inheritdoc />
        protected override void DisposeUnderlyingTransaction()
        {
            UnderlyingTransaction.Dispose();
        }
    }
}
