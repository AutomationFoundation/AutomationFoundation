using System.Data.SqlClient;
using AutomationFoundation.Extensions.SystemData.Primitives;

namespace AutomationFoundation.Extensions.SqlClient.Primitives
{
    internal class SqlTransactionWrapper : DbTransactionWrapper<SqlTransaction>, ISqlTransactionWrapper
    {
        public SqlTransactionWrapper(SqlTransaction transaction)
            : base(transaction)
        {
        }

        public void Rollback(string savePointName)
        {
            UnderlyingTransaction.Rollback(savePointName);
        }

        public void Save(string savePointName)
        {
            UnderlyingTransaction.Save(savePointName);
        }
    }
}