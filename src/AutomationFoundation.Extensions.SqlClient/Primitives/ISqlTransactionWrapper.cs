using System.Data.SqlClient;
using AutomationFoundation.Extensions.SystemData.Primitives;

/* Unmerged change from project 'AutomationFoundation.Extensions.SqlClient(net472)'
Before:
namespace AutomationFoundation.Extensions.SqlClient.Primitives
{
    internal interface ISqlTransactionWrapper : IDbTransactionWrapper<SqlTransaction>
    {
        void Rollback(string savePointName);

        void Save(string savePointName);
    }
After:
namespace AutomationFoundation.Extensions.SqlClient.Primitives;

internal interface ISqlTransactionWrapper : IDbTransactionWrapper<SqlTransaction>
{
    void Rollback(string savePointName);

    void Save(string savePointName);
*/

namespace AutomationFoundation.Extensions.SqlClient.Primitives;

internal interface ISqlTransactionWrapper : IDbTransactionWrapper<SqlTransaction>
{
    void Rollback(string savePointName);

    void Save(string savePointName);
}