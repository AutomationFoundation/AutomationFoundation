using System;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using AutomationFoundation.Extensions.SystemTransactions.Primitives;
using AutomationFoundation.Transactions.Abstractions;

namespace AutomationFoundation.Extensions.SystemTransactions
{
    /// <summary>
    /// Provides an adapter for a <see cref="CommittableTransaction"/> transaction.
    /// </summary>
    /// <typeparam name="TWrapper">The type of wrapper for the transaction.</typeparam>
    /// <typeparam name="TTransaction">The type of transaction being adapted.</typeparam>
    public abstract class TransactionAdapter<TWrapper, TTransaction> : BaseTransactionAdapter<TTransaction>
        where TWrapper : TransactionWrapper<TTransaction>
        where TTransaction : Transaction
    {
        /// <summary>
        /// Gets the transaction being adapted by this instance.
        /// </summary>
        protected TWrapper Transaction { get; }

        /// <inheritdoc />
        public override TTransaction UnderlyingTransaction => Transaction.UnderlyingTransaction;

        /// <summary>
        /// Initializes an instance of the <see cref="TransactionAdapter{TWrapper, TTransaction}"/> class.
        /// </summary>
        /// <param name="transaction">The transaction which is being adapted.</param>
        /// <param name="ownsTransaction">Optional. Identifies whether the adapter will take ownership of the transaction.</param>
        protected TransactionAdapter(TWrapper transaction, bool ownsTransaction = true)
            : base(ownsTransaction)
        {
            Transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }

        /// <inheritdoc />
        public override Task RollbackAsync(CancellationToken cancellationToken)
        {
            GuardMustNotBeDisposed();

            Transaction.Rollback();

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        protected override void ReleaseUnderlyingTransaction()
        {
            Transaction.Dispose();
        }
    }
}