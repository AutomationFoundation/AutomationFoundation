using System.Transactions;
using AutomationFoundation.Extensions.SystemTransactions.Primitives;

namespace AutomationFoundation.Extensions.SystemTransactions
{
    /// <summary>
    /// Provides an adapter for a <see cref="CommittableTransaction"/> which can be used by the framework.
    /// </summary>
    public class CommittableTransactionAdapter : TransactionAdapter<CommittableTransactionWrapper, CommittableTransaction>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommittableTransactionAdapter"/> class.
        /// </summary>
        /// <param name="transaction">The transaction which is being adapted.</param>
        /// <param name="ownsTransaction">Optional. Identifies whether the adapter will take ownership of the transaction.</param>
        public CommittableTransactionAdapter(CommittableTransaction transaction, bool ownsTransaction = true)
            : base(new CommittableTransactionWrapper(transaction), ownsTransaction)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommittableTransactionAdapter"/> class.
        /// </summary>
        /// <param name="transaction">The wrapped transaction which is being adapted.</param>
        /// <param name="ownsTransaction">Optional. Identifies whether the adapter will take ownership of the transaction.</param>
        protected CommittableTransactionAdapter(CommittableTransactionWrapper transaction, bool ownsTransaction)
            : base(transaction, ownsTransaction)
        {
        }

        /// <inheritdoc />
        public override void Commit()
        {
            Transaction.Commit();
        }
    }
}