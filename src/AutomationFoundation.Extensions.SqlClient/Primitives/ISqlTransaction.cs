using System.Data.SqlClient;
using AutomationFoundation.Extensions.SystemData.Primitives;

namespace AutomationFoundation.Extensions.SqlClient.Primitives
{
    /// <summary>
    /// INFRASTRUCTURE ONLY: This interface is not intended to be used within your application code, use at your own risk!
    /// </summary>
    public interface ISqlTransaction : IDbTransactionWrapper<SqlTransaction>
    {
        /// <summary>
        /// Rolls the transaction back to the original state.
        /// </summary>
        /// <param name="savePointName">The save point name which to rollback.</param>
        void Rollback(string savePointName);

        /// <summary>
        /// Saves a save point.
        /// </summary>
        /// <param name="savePointName">The save point name to save.</param>
        void Save(string savePointName);
    }
}