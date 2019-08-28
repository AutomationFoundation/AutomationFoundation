using System.Transactions;
using AutomationFoundation.Extensions.SystemTransactions.Primitives;

namespace AutomationFoundation.Extensions.SystemTransactions
{
    /// <summary>
    /// Provides an adapter for a <see cref="DependentTransaction"/> which can be used by the framework.
    /// </summary>
    public class DependentTransactionAdapter : TransactionAdapter<DependentTransactionWrapper, DependentTransaction>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DependentTransactionAdapter"/> class.
        /// </summary>
        /// <param name="transaction">The transaction which is being adapted.</param>
        /// <param name="ownsTransaction">Optional. Identifies whether the adapter will take ownership of the transaction.</param>
        public DependentTransactionAdapter(DependentTransaction transaction, bool ownsTransaction = true)
            : this(new DependentTransactionWrapper(transaction), ownsTransaction)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DependentTransactionAdapter"/> class.
        /// </summary>
        /// <param name="transaction">The wrapped transaction which is being adapted.</param>
        /// <param name="ownsTransaction">Optional. Identifies whether the adapter will take ownership of the transaction.</param>
        protected DependentTransactionAdapter(DependentTransactionWrapper transaction, bool ownsTransaction)
            : base(transaction, ownsTransaction)
        {
        }

        /// <inheritdoc />
        public override void Commit()
        {
            Transaction.Complete();
        }
    }
}