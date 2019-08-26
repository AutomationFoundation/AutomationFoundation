using System;
using System.Transactions;
using AutomationFoundation.Extensions.SystemTransactions.Primitives;
using AutomationFoundation.Transactions.Abstractions;

namespace AutomationFoundation.Extensions.SystemTransactions
{
    /// <summary>
    /// Provides an adapter for a <see cref="CommittableTransaction"/> transaction.
    /// </summary>
    public sealed class CommittableTransactionAdapter : BaseTransactionAdapter<CommittableTransaction>
    {
        private readonly CommittableTransactionWrapper transaction;

        /// <inheritdoc />
        public override CommittableTransaction UnderlyingTransaction => transaction.UnderlyingTransaction;

        /// <summary>
        /// Initializes an instance of the <see cref="CommittableTransactionAdapter"/> class.
        /// </summary>
        /// <param name="transaction">The transaction which is being adapted.</param>
        /// <param name="ownsTransaction">Optional. Identifies whether the adapter will take ownership of the transaction.</param>
        public CommittableTransactionAdapter(CommittableTransaction transaction, bool ownsTransaction = true)
            : base(ownsTransaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            this.transaction = new CommittableTransactionWrapper(transaction);
        }

        internal CommittableTransactionAdapter(CommittableTransactionWrapper transaction, bool ownsTransaction = true)
            : base(ownsTransaction)
        {
            this.transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }

        /// <inheritdoc />
        public override void Commit()
        {
            GuardMustNotBeDisposed();

            transaction.Commit();
        }

        /// <inheritdoc />
        public override void Rollback()
        {
            GuardMustNotBeDisposed();

            transaction.Rollback();
        }

        /// <inheritdoc />
        protected override void DisposeUnderlyingTransaction()
        {
            transaction.Dispose();
        }
    }
}