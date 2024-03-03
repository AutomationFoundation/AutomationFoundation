using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using AutomationFoundation.Extensions.SystemTransactions.Primitives;

namespace AutomationFoundation.Extensions.SystemTransactions.TestObjects;

public class StubTransactionAdapter : TransactionAdapter<CommittableTransactionWrapper, CommittableTransaction>
{
    public StubTransactionAdapter(CommittableTransactionWrapper transaction, bool ownsTransaction = true)
        : base(transaction, ownsTransaction)
    {
    }

    public override void Commit()
    {
    }

    public override Task CommitAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}