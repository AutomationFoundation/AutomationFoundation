using System;
using System.Transactions;

namespace AutomationFoundation.Extensions.SystemTransactions.Primitives
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S3881:\"IDisposable\" should be implemented correctly",
        Justification = "False Positive")]
    internal class CommittableTransactionWrapper : IDisposable
    {
        public virtual CommittableTransaction UnderlyingTransaction { get; }

        public CommittableTransactionWrapper()
        {
        }

        public CommittableTransactionWrapper(CommittableTransaction transaction)
        {
            UnderlyingTransaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }

        ~CommittableTransactionWrapper()
        {
            Dispose(false);
        }

        public virtual void Commit()
        {
            UnderlyingTransaction.Commit();
        }

        public virtual void Rollback()
        {
            UnderlyingTransaction.Rollback();
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
                UnderlyingTransaction.Dispose();
            }
        }
    }
}