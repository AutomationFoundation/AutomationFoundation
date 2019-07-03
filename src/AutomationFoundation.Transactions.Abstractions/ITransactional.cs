namespace AutomationFoundation.Transactions.Abstractions
{
    /// <summary>
    /// Identifies an object which supports transactions.
    /// </summary>
    public interface ITransactional
    {
        /// <summary>
        /// Begins a transaction.
        /// </summary>
        /// <returns>The transaction which was created.</returns>
        ITransaction BeginTransaction();
    }
}