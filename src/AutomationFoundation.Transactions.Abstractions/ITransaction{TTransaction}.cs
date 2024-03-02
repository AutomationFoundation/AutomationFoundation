namespace AutomationFoundation.Transactions.Abstractions;

/// <summary>
/// Identifies a transaction.
/// </summary>
/// <typeparam name="TTransaction">The type of the underlying transaction.</typeparam>
public interface ITransaction<out TTransaction> : ITransaction
{
    /// <summary>
    /// Gets the underlying transaction.
    /// </summary>
    TTransaction UnderlyingTransaction { get; }
}