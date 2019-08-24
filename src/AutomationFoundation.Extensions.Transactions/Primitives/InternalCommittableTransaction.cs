using System;
using System.Transactions;

namespace AutomationFoundation.Extensions.Transactions.Primitives
{
    internal class InternalCommittableTransaction : IDisposable
    {
        public virtual CommittableTransaction UnderlyingTransaction { get; }

        public InternalCommittableTransaction()
        {
        }

        public InternalCommittableTransaction(CommittableTransaction transaction)
        {
            UnderlyingTransaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }

        ~InternalCommittableTransaction()
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