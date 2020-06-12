using System;
using System.Data.Entity;

namespace AutomationFoundation.Extensions.EntityFramework.Primitives
{
    /// <summary>
    /// INFRASTRUCTURE ONLY: This interface is not intended to be used within your application code, use at your own risk!
    /// </summary>
    public class DbContextTransactionWrapper : IDbContextTransaction
    {
        /// <summary>
        /// Gets the underlying transaction.
        /// </summary>
        public DbContextTransaction UnderlyingTransaction { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextTransactionWrapper"/> class.
        /// </summary>
        /// <param name="transaction">The database transaction being wrapped.</param>
        public DbContextTransactionWrapper(DbContextTransaction transaction)
        {
            UnderlyingTransaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="DbContextTransactionWrapper"/> class.
        /// </summary>
        ~DbContextTransactionWrapper()
        {
            Dispose(false);
        }

        /// <inheritdoc />
        public void Commit()
        {
            UnderlyingTransaction.Commit();
        }

        /// <inheritdoc />
        public void Rollback()
        {
            UnderlyingTransaction.Rollback();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources, otherwise false to release unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnderlyingTransaction.Dispose();
            }
        }
    }
}