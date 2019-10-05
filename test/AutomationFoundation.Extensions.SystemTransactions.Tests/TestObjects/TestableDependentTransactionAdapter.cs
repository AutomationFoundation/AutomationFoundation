using AutomationFoundation.Extensions.SystemTransactions.Primitives;

namespace AutomationFoundation.Extensions.SystemTransactions.Stubs
{
    public class TestableDependentTransactionAdapter : DependentTransactionAdapter
    {
        public TestableDependentTransactionAdapter(DependentTransactionWrapper transaction, bool ownsTransaction) 
            : base(transaction, ownsTransaction)
        {
        }
    }
}
