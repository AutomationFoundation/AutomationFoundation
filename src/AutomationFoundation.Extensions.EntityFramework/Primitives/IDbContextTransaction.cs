using System;
using System.Data.Entity;

namespace AutomationFoundation.Extensions.EntityFramework.Primitives
{
    /// <summary>
    /// INFRASTRUCTURE ONLY: This interface is not intended to be used within your application code, use at your own risk!
    /// </summary>
    public interface IDbContextTransaction : IDisposable
    {
        /// <summary>
        /// Gets the underlying database context transaction.
        /// </summary>
        DbContextTransaction UnderlyingTransaction { get; }

        /// <summary>
        /// Commits the transactions.
        /// </summary>
        void Commit();

        /// <summary>
        /// Rolls the transaction back to the original state.
        /// </summary>
        void Rollback();
    }
}