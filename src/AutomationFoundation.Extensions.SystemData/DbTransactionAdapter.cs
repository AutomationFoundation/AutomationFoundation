using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Extensions.SystemData.Primitives;
using AutomationFoundation.Transactions.Abstractions;

namespace AutomationFoundation.Extensions.SystemData;

/// <summary>
/// Provides an adapter for a <see cref="DbTransaction"/> transaction.
/// </summary>
/// <typeparam name="TTransaction">The type of transaction being adapted.</typeparam>
public class DbTransactionAdapter<TTransaction> : BaseTransactionAdapter<TTransaction>
    where TTransaction : DbTransaction
{
    /// <summary>
    /// Initializes an instance of the <see cref="DbTransactionAdapter{TTransaction}"/> class.
    /// </summary>
    /// <param name="transaction">The transaction which is being adapted.</param>
    /// <param name="ownsTransaction">Optional. Identifies whether the adapter will take ownership of the transaction.</param>
    public DbTransactionAdapter(TTransaction transaction, bool ownsTransaction = true)
        : this(new DbTransactionWrapper<TTransaction>(transaction), ownsTransaction)
    {
    }

    /// <summary>
    /// Initializes an instance of the <see cref="DbTransactionAdapter{TTransaction}"/> class.
    /// </summary>
    /// <param name="transaction">The wrapped transaction which is being adapted.</param>
    /// <param name="ownsTransaction">Optional. Identifies whether the adapter will take ownership of the transaction.</param>
    public DbTransactionAdapter(IDbTransactionWrapper<TTransaction> transaction, bool ownsTransaction)
        : base(ownsTransaction)
    {
        Transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
    }

    /// <inheritdoc />
    public override TTransaction UnderlyingTransaction => Transaction.UnderlyingTransaction;

    /// <summary>
    /// Gets the wrapped transaction being held by the adapter.
    /// </summary>
    protected IDbTransactionWrapper<TTransaction> Transaction { get; }

    /// <inheritdoc />
    public override void Rollback()
    {
        GuardMustNotBeDisposed();

        Transaction.Rollback();
    }

    /// <inheritdoc />
    public override Task RollbackAsync(CancellationToken cancellationToken)
    {
        Rollback();

        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public override void Commit()
    {
        GuardMustNotBeDisposed();

        Transaction.Commit();
    }

    /// <inheritdoc />
    public override Task CommitAsync(CancellationToken cancellationToken)
    {
        Commit();

        return Task.CompletedTask;
    }

    /// <inheritdoc />
    protected override void ReleaseUnderlyingTransaction()
    {
        Transaction.Dispose();
    }
}