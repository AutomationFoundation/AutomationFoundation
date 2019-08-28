using System.Transactions;

namespace AutomationFoundation.Extensions.SystemTransactions.Primitives
{
    /// <summary>
    /// Provides a wrapper for a <see cref="CommittableTransaction"/> instance.
    /// </summary>
    public class CommittableTransactionWrapper : TransactionWrapper<CommittableTransaction>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommittableTransactionWrapper"/> class.
        /// </summary>
        public CommittableTransactionWrapper()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommittableTransactionWrapper"/> class.
        /// </summary>
        /// <param name="transaction">The transaction being wrapped.</param>
        public CommittableTransactionWrapper(CommittableTransaction transaction)
            : base(transaction)
        {
        }

        /// <summary>
        /// Attempts to commit the transaction.
        /// </summary>
        public virtual void Commit()
        {
            UnderlyingTransaction.Commit();
        }
    }
}