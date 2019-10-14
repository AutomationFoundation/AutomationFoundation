using AutomationFoundation.Extensions.SystemTransactions.Primitives;

namespace AutomationFoundation.Extensions.SystemTransactions.TestObjects
{
    public class TestableDependentTransactionAdapter : DependentTransactionAdapter
    {
        public TestableDependentTransactionAdapter(DependentTransactionWrapper transaction, bool ownsTransaction) 
            : base(transaction, ownsTransaction)
        {
        }
    }
}
