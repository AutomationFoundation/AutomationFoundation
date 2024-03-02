using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Extensions.SystemTransactions.Primitives;
using AutomationFoundation.Extensions.SystemTransactions.TestObjects;
using Moq;
using NUnit.Framework;

/* Unmerged change from project 'AutomationFoundation.Extensions.SystemTransactions.Tests(net472)'
Before:
namespace AutomationFoundation.Extensions.SystemTransactions
{
    [TestFixture]
    public class DependentTransactionAdapterTests
    {
        private Mock<DependentTransactionWrapper> transaction;

        [SetUp]
        public void Setup()
        {
            transaction = new Mock<DependentTransactionWrapper>();
        }

        [Test]
        public async Task MustCompleteTheTransaction()
        {
            using (var target = new TestableDependentTransactionAdapter(transaction.Object, true))
            {
                await target.CommitAsync(CancellationToken.None);
            }

            transaction.Verify(o => o.Complete(), Times.Once);
        }
After:
namespace AutomationFoundation.Extensions.SystemTransactions;

[TestFixture]
public class DependentTransactionAdapterTests
{
    private Mock<DependentTransactionWrapper> transaction;

    [SetUp]
    public void Setup()
    {
        transaction = new Mock<DependentTransactionWrapper>();
    }

    [Test]
    public async Task MustCompleteTheTransaction()
    {
        using (var target = new TestableDependentTransactionAdapter(transaction.Object, true))
        {
            await target.CommitAsync(CancellationToken.None);
        }

        transaction.Verify(o => o.Complete(), Times.Once);
*/

namespace AutomationFoundation.Extensions.SystemTransactions;

[TestFixture]
public class DependentTransactionAdapterTests
{
    private Mock<DependentTransactionWrapper> transaction;

    [SetUp]
    public void Setup()
    {
        transaction = new Mock<DependentTransactionWrapper>();
    }

    [Test]
    public async Task MustCompleteTheTransaction()
    {
        using (var target = new TestableDependentTransactionAdapter(transaction.Object, true))
        {
            await target.CommitAsync(CancellationToken.None);
        }

        transaction.Verify(o => o.Complete(), Times.Once);
    }
}