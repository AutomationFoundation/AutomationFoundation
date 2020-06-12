using System;
using System.Data.Common;

namespace AutomationFoundation.Extensions.SystemData.Primitives
{
    /// <summary>
    /// INFRASTRUCTURE ONLY: This interface is not intended to be used within your application code, use at your own risk!
    /// </summary>
    /// <typeparam name="TTransaction">The type transaction being wrapped.</typeparam>
    public interface IDbTransactionWrapper<out TTransaction> : IDisposable
        where TTransaction : DbTransaction
    {
        /// <summary>
        /// Gets the underlying transaction.
        /// </summary>
        TTransaction UnderlyingTransaction { get; }

        /// <summary>
        /// Commits the transaction.
        /// </summary>
        void Commit();

        /// <summary>
        /// Rolls the transaction back to the original state.
        /// </summary>
        void Rollback();
    }
}