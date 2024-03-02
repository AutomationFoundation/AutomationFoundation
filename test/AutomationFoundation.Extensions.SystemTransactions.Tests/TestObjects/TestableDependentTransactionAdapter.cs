using AutomationFoundation.Extensions.SystemTransactions.Primitives;


/* Unmerged change from project 'AutomationFoundation.Extensions.SystemTransactions.Tests(net472)'
Before:
namespace AutomationFoundation.Extensions.SystemTransactions.TestObjects
{
    public class TestableDependentTransactionAdapter : DependentTransactionAdapter
    {
        public TestableDependentTransactionAdapter(DependentTransactionWrapper transaction, bool ownsTransaction) 
            : base(transaction, ownsTransaction)
        {
        }
After:
namespace AutomationFoundation.Extensions.SystemTransactions.TestObjects;

public class TestableDependentTransactionAdapter : DependentTransactionAdapter
{
    public TestableDependentTransactionAdapter(DependentTransactionWrapper transaction, bool ownsTransaction) 
        : base(transaction, ownsTransaction)
    {
*/
namespace AutomationFoundation.Extensions.SystemTransactions.TestObjects;

public class TestableDependentTransactionAdapter : DependentTransactionAdapter
{
    public TestableDependentTransactionAdapter(DependentTransactionWrapper transaction, bool ownsTransaction)
        : base(transaction, ownsTransaction)
    {
    }
}