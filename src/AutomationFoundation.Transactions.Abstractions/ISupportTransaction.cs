using System.Threading;
using System.Threading.Tasks;

namespace AutomationFoundation.Transactions.Abstractions
{
    /// <summary>
    /// Identifies an object which supports transactions.
    /// </summary>
    public interface ISupportTransaction
    {
        /// <summary>
        /// Begins a transaction.
        /// </summary>
        /// <returns>The transaction which was started.</returns>
        ITransaction BeginTransaction();

        /// <summary>
        /// Begins a transaction.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to monitor for cancellation requests.</param>
        /// <returns>The transaction which was started.</returns>
        Task<ITransaction> BeginTransactionAsync(CancellationToken cancellationToken);
    }
}