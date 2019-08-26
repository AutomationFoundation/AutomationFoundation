using System;
using System.Data.Entity;
using AutomationFoundation.Extensions.EntityFramework.Primitives;
using AutomationFoundation.Transactions.Abstractions;

namespace AutomationFoundation.Extensions.EntityFramework
{
    /// <summary>
    /// Provides an adapter for a <see cref="DbContextTransaction"/> transaction.
    /// </summary>
    public sealed class DbContextTransactionAdapter : BaseTransactionAdapter<DbContextTransaction>
    {
        private readonly DbContextTransactionWrapper transaction;

        /// <summary>
        /// Initializes an instance of the <see cref="DbContextTransactionAdapter"/> class.
        /// </summary>
        /// <param name="transaction">The transaction which is being adapted.</param>
        /// <param name="ownsTransaction">Optional. Identifies whether the adapter will take ownership of the transaction.</param>
        public DbContextTransactionAdapter(DbContextTransaction transaction, bool ownsTransaction = true) 
            : base(ownsTransaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            this.transaction = new DbContextTransactionWrapper(transaction);
        }

        internal DbContextTransactionAdapter(DbContextTransactionWrapper transaction, bool ownsTransaction = true)
            : base(ownsTransaction)
        {
            this.transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }

        /// <inheritdoc />
        public override DbContextTransaction UnderlyingTransaction => transaction.UnderlyingTransaction;

        /// <inheritdoc />
        public override void Rollback()
        {
            GuardMustNotBeDisposed();

            transaction.Rollback();
        }

        /// <inheritdoc />
        public override void Commit()
        {
            GuardMustNotBeDisposed();

            transaction.Commit();
        }

        /// <inheritdoc />
        protected override void DisposeUnderlyingTransaction()
        {
            transaction.Dispose();
        }
    }
}