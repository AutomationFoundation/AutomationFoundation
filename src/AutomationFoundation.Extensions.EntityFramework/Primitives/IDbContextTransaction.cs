using System;
using System.Data.Entity;

namespace AutomationFoundation.Extensions.EntityFramework.Primitives
{
    /// <summary>
    /// Identifies an Entity Framework compatible database transaction.
    /// </summary>
    internal interface IDbContextTransaction : IDisposable
    {
        /// <summary>
        /// Gets the underling database context transaction.
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