namespace AutomationFoundation.Runtime
{
    /// <summary>
    /// Identifies a strategy for handling multiple error handling strategies.
    /// </summary>
    public interface ICompositeErrorHandlingStrategy : IErrorHandlingStrategy
    {
        /// <summary>
        /// Adds a strategy.
        /// </summary>
        /// <param name="strategy">The strategy to add to the composite strategy.</param>
        void AddStrategy(IErrorHandlingStrategy strategy);

        /// <summary>
        /// Removes a strategy.
        /// </summary>
        /// <param name="strategy">The strategy to remove from the composite strategy.</param>
        void RemoveStrategy(IErrorHandlingStrategy strategy);
    }
}