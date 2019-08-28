using System.Transactions;

namespace AutomationFoundation.Extensions.SystemTransactions.Primitives
{
    /// <summary>
    /// Provides a wrapper for a <see cref="DependentTransaction"/> instance.
    /// </summary>
    public class DependentTransactionWrapper : TransactionWrapper<DependentTransaction>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DependentTransactionWrapper"/> class.
        /// </summary>
        public DependentTransactionWrapper()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DependentTransactionWrapper"/> class.
        /// </summary>
        /// <param name="transaction">The transaction being wrapped.</param>
        public DependentTransactionWrapper(DependentTransaction transaction) 
            : base(transaction)
        {
        }

        /// <summary>
        /// Attempts to complete the dependent transaction.
        /// </summary>
        public virtual void Complete()
        {
            UnderlyingTransaction.Complete();
        }
    }
}