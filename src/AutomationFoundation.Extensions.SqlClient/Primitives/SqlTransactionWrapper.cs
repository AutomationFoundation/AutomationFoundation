using System.Data.SqlClient;
using AutomationFoundation.Extensions.SystemData.Primitives;

/* Unmerged change from project 'AutomationFoundation.Extensions.SqlClient(net472)'
Before:
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
After:
namespace AutomationFoundation.Extensions.SqlClient.Primitives;

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
*/

namespace AutomationFoundation.Extensions.SqlClient.Primitives;

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