using System.Data.SqlClient;
using AutomationFoundation.Extensions.SystemData.Primitives;

namespace AutomationFoundation.Extensions.SqlClient.Primitives
{
    internal interface ISqlTransactionWrapper : IDbTransactionWrapper<SqlTransaction>
    {
        void Rollback(string savePointName);

        void Save(string savePointName);
    }
}