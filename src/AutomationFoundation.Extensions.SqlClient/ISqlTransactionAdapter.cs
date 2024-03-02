/* Unmerged change from project 'AutomationFoundation.Extensions.SqlClient(net472)'
Before:
namespace AutomationFoundation.Extensions.SqlClient
{
    /// <summary>
    /// Identifies an <see cref="SqlTransaction"/> adapter.
    /// </summary>
    public interface ISqlTransactionAdapter : ITransaction<SqlTransaction>
    {
        /// <summary>
        /// Rolls the transaction back to the savepoint specified.
        /// </summary>
        /// <param name="savePointName">The name of the transaction or savepoint.</param>
        void Rollback(string savePointName);

        /// <summary>
        /// Begins a savepoint within the transaction with the specified name.
        /// </summary>
        /// <param name="savePointName">The name of the savepoint to start within the transaction.</param>
        void Save(string savePointName);
    }
After:
namespace AutomationFoundation.Extensions.SqlClient;

/// <summary>
/// Identifies an <see cref="SqlTransaction"/> adapter.
/// </summary>
public interface ISqlTransactionAdapter : ITransaction<SqlTransaction>
{
    /// <summary>
    /// Rolls the transaction back to the savepoint specified.
    /// </summary>
    /// <param name="savePointName">The name of the transaction or savepoint.</param>
    void Rollback(string savePointName);

    /// <summary>
    /// Begins a savepoint within the transaction with the specified name.
    /// </summary>
    /// <param name="savePointName">The name of the savepoint to start within the transaction.</param>
    void Save(string savePointName);
*/
using System.Data.SqlClient;
using AutomationFoundation.Transactions.Abstractions;

namespace AutomationFoundation.Extensions.SqlClient;

/// <summary>
/// Identifies an <see cref="SqlTransaction"/> adapter.
/// </summary>
public interface ISqlTransactionAdapter : ITransaction<SqlTransaction>
{
    /// <summary>
    /// Rolls the transaction back to the savepoint specified.
    /// </summary>
    /// <param name="savePointName">The name of the transaction or savepoint.</param>
    void Rollback(string savePointName);

    /// <summary>
    /// Begins a savepoint within the transaction with the specified name.
    /// </summary>
    /// <param name="savePointName">The name of the savepoint to start within the transaction.</param>
    void Save(string savePointName);
}