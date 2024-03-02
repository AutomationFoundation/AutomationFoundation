namespace AutomationFoundation.Transactions.Abstractions;

/// <summary>
/// Identifies an object which holds a transaction.
/// </summary>
public interface ITransactionContainer
{
    /// <summary>
    /// Gets the transaction.
    /// </summary>
    ITransaction Transaction { get; }
}