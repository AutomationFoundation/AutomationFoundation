using System;
using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Transactions.Abstractions;
using Microsoft.EntityFrameworkCore.Storage;

namespace AutomationFoundation.Extensions.EntityFrameworkCore
{
    /// <summary>
    /// Provides an adapter for a <see cref="IDbContextTransaction"/> transaction.
    /// </summary>
    public class DbContextTransactionAdapter : BaseTransactionAdapter<IDbContextTransaction>
    {
        private readonly IDbContextTransaction transaction;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextTransactionAdapter"/> class.
        /// </summary>
        /// <param name="transaction">The transaction which is being adapted.</param>
        /// <param name="ownsTransaction">Optional. Identifies whether the adapter will take ownership of the transaction.</param>
        public DbContextTransactionAdapter(IDbContextTransaction transaction, bool ownsTransaction = true) 
            : base(ownsTransaction)
        {
            this.transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }

        /// <inheritdoc />
        public override IDbContextTransaction UnderlyingTransaction => transaction;

        /// <inheritdoc />
        public override void Rollback()
        {
            GuardMustNotBeDisposed();

            transaction.Rollback();
        }

        /// <inheritdoc />
        public override async Task RollbackAsync(CancellationToken cancellationToken)
        {
            GuardMustNotBeDisposed();

            await transaction.RollbackAsync(cancellationToken);
        }

        /// <inheritdoc />
        public override void Commit()
        {
            GuardMustNotBeDisposed();

            transaction.Commit();
        }

        /// <inheritdoc />
        public override async Task CommitAsync(CancellationToken cancellationToken)
        {
            GuardMustNotBeDisposed();

            await transaction.CommitAsync(cancellationToken);
        }

        /// <inheritdoc />
        protected override void ReleaseUnderlyingTransaction()
        {
            transaction.Dispose();
        }
    }
}
