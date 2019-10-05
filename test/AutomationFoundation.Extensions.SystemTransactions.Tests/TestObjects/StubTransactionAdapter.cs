using System.Transactions;
using AutomationFoundation.Extensions.SystemTransactions.Primitives;

namespace AutomationFoundation.Extensions.SystemTransactions.Stubs
{
    public class StubTransactionAdapter : TransactionAdapter<CommittableTransactionWrapper, CommittableTransaction>
    {
        public StubTransactionAdapter(CommittableTransactionWrapper transaction, bool ownsTransaction = true)
            : base(transaction, ownsTransaction)
        {
        }

        public override void Commit()
        {
        }
    }
}