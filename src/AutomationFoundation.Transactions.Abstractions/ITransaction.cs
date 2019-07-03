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
        /// Rolls the transaction back to the original state.
        /// </summary>
        void Rollback();
    }
}