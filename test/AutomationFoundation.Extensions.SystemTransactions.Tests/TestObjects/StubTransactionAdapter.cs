using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using AutomationFoundation.Extensions.SystemTransactions.Primitives;

/* Unmerged change from project 'AutomationFoundation.Extensions.SystemTransactions.Tests(net472)'
Before:
namespace AutomationFoundation.Extensions.SystemTransactions.TestObjects
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

        public override Task CommitAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
After:
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
*/

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