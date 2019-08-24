using System;
using System.Data.Entity;

namespace AutomationFoundation.Extensions.EntityFramework.Primitives
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S3881:\"IDisposable\" should be implemented correctly", 
        Justification = "False Positive")]
    internal class DbContextTransactionWrapper : IDisposable
    {
        private readonly DbContextTransaction transaction;

        public virtual DbContextTransaction UnderlyingTransaction => transaction;

        public DbContextTransactionWrapper()
        {
        }

        public DbContextTransactionWrapper(DbContextTransaction transaction)
        {
            this.transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }

        ~DbContextTransactionWrapper()
        {
            Dispose(false);
        }

        public virtual void Commit()
        {
            transaction.Commit();
        }

        public virtual void Rollback()
        {
            transaction.Rollback();
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                transaction.Dispose();
            }
        }
    }
}