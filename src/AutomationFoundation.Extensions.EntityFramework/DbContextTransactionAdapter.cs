using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Extensions.EntityFramework.Primitives;
using AutomationFoundation.Transactions.Abstractions;

namespace AutomationFoundation.Extensions.EntityFramework
{
    /// <summary>
    /// Provides an adapter for a <see cref="DbContextTransaction"/> transaction.
    /// </summary>
    public sealed class DbContextTransactionAdapter : BaseTransactionAdapter<DbContextTransaction>
    {
        private readonly IDbContextTransaction transaction;

        /// <summary>
        /// Initializes an instance of the <see cref="DbContextTransactionAdapter"/> class.
        /// </summary>
        /// <param name="transaction">The transaction which is being adapted.</param>
        /// <param name="ownsTransaction">Optional. Identifies whether the adapter will take ownership of the transaction.</param>
        public DbContextTransactionAdapter(DbContextTransaction transaction, bool ownsTransaction = true)
            : this(new DbContextTransactionWrapper(transaction), ownsTransaction)
        {
        }

        /// <summary>
        /// Initializes an instance of the <see cref="DbContextTransactionAdapter"/> class.
        /// </summary>
        /// <param name="transaction">The transaction which is being adapted.</param>
        /// <param name="ownsTransaction">Optional. Identifies whether the adapter will take ownership of the transaction.</param>
        internal DbContextTransactionAdapter(IDbContextTransaction transaction, bool ownsTransaction = true)
            : base(ownsTransaction)
        {
            this.transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }

        /// <inheritdoc />
        public override DbContextTransaction UnderlyingTransaction => transaction.UnderlyingTransaction;

        /// <inheritdoc />
        public override Task RollbackAsync(CancellationToken cancellationToken)
        {
            GuardMustNotBeDisposed();

            transaction.Rollback();

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public override Task CommitAsync(CancellationToken cancellationToken)
        {
            GuardMustNotBeDisposed();

            transaction.Commit();

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        protected override void ReleaseUnderlyingTransaction()
        {
            transaction.Dispose();
        }
    }
}