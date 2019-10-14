using System.Threading;
using System.Threading.Tasks;

namespace AutomationFoundation.Transactions.Abstractions
{
    /// <summary>
    /// Identifies a transaction.
    /// </summary>
    public interface ITransaction
    {
        /// <summary>
        /// Commits the transaction.
        /// </summary>
        void Commit();

        /// <summary>
        /// Commits the transaction.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to monitor for cancellation requests.</param>
        /// <returns>The task to await.</returns>
        Task CommitAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Rolls the transaction back to the original state.
        /// </summary>
        void Rollback();

        /// <summary>
        /// Rolls the transaction back to the original state.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to monitor for cancellation requests.</param>
        /// <returns>The task to await.</returns>
        Task RollbackAsync(CancellationToken cancellationToken);
    }
}