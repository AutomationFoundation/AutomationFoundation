using AutomationFoundation.Extensions.SystemTransactions.Primitives;

namespace AutomationFoundation.Extensions.SystemTransactions.TestObjects
{
    public class TestableCommittableTransactionAdapter : CommittableTransactionAdapter
    {
        public TestableCommittableTransactionAdapter(CommittableTransactionWrapper transaction, bool ownsTransaction) 
            : base(transaction, ownsTransaction)
        {
        }
    }
}