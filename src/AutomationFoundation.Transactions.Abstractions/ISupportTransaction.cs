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
    }
}