using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Extensions.SystemData.Primitives;
using AutomationFoundation.Transactions.Abstractions;

namespace AutomationFoundation.Extensions.SystemData
{
    /// <summary>
    /// Provides an adapter for a <see cref="DbTransaction"/> transaction.
    /// </summary>
    /// <typeparam name="TTransaction">The type of transaction being adapted.</typeparam>
    public class DbTransactionAdapter<TTransaction> : DbTransactionAdapter<TTransaction, DbTransactionWrapper<TTransaction>>
        where TTransaction : DbTransaction
    {
        /// <summary>
        /// Initializes an instance of the <see cref="DbTransactionAdapter{TTransaction, TWrapper}"/> class.
        /// </summary>
        /// <param name="transaction">The transaction which is being adapted.</param>
        /// <param name="ownsTransaction">Optional. Identifies whether the adapter will take ownership of the transaction.</param>
        public DbTransactionAdapter(TTransaction transaction, bool ownsTransaction = true)
            : base(new DbTransactionWrapper<TTransaction>(transaction), ownsTransaction)
        {
        }
    }

    /// <summary>
    /// Provides an adapter for a <see cref="DbTransaction"/> transaction.
    /// </summary>
    /// <typeparam name="TTransaction">The type of transaction being adapted.</typeparam>
    /// <typeparam name="TWrapper">The type of wrapper surrounding the transaction.</typeparam>
    public abstract class DbTransactionAdapter<TTransaction, TWrapper> : BaseTransactionAdapter<TTransaction>
        where TTransaction : DbTransaction
        where TWrapper : class, IDbTransactionWrapper<TTransaction>
    {        
        /// <summary>
        /// Initializes an instance of the <see cref="DbTransactionAdapter{TTransaction, TWrapper}"/> class.
        /// </summary>
        /// <param name="transaction">The wrapped transaction which is being adapted.</param>
        /// <param name="ownsTransaction">Optional. Identifies whether the adapter will take ownership of the transaction.</param>
        protected DbTransactionAdapter(TWrapper transaction, bool ownsTransaction)
            : base(ownsTransaction)
        {
            Transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }

        /// <inheritdoc />
        public override TTransaction UnderlyingTransaction => Transaction.UnderlyingTransaction;

        /// <summary>
        /// Gets the wrapped transaction being held by the adapter.
        /// </summary>
        protected TWrapper Transaction { get; }

        /// <inheritdoc />
        public override Task RollbackAsync(CancellationToken cancellationToken)
        {
            GuardMustNotBeDisposed();

            Transaction.Rollback();

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public override Task CommitAsync(CancellationToken cancellationToken)
        {
            GuardMustNotBeDisposed();

            Transaction.Commit();
            
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        protected override void ReleaseUnderlyingTransaction()
        {
            Transaction.Dispose();
        }
    }
}
