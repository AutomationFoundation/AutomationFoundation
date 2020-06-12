using System.Data.SqlClient;
using AutomationFoundation.Extensions.SystemData.Primitives;

namespace AutomationFoundation.Extensions.SqlClient.Primitives
{
    /// <summary>
    /// INFRASTRUCTURE ONLY: This class is not intended for use within your application code, use at your own risk!
    /// </summary>
    public class SqlTransactionWrapper : DbTransactionWrapper<SqlTransaction>, ISqlTransaction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlTransactionWrapper"/> class.
        /// </summary>
        /// <param name="transaction">The transaction being wrapped.</param>
        public SqlTransactionWrapper(SqlTransaction transaction)
            : base(transaction)
        {
        }

        /// <inheritdoc />
        public void Rollback(string savePointName)
        {
            UnderlyingTransaction.Rollback(savePointName);
        }

        /// <inheritdoc />
        public void Save(string savePointName)
        {
            UnderlyingTransaction.Save(savePointName);
        }
    }
}