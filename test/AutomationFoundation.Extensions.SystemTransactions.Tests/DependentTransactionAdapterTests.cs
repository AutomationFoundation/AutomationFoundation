using AutomationFoundation.Extensions.SystemTransactions.Primitives;
using AutomationFoundation.Extensions.SystemTransactions.Stubs;
using Moq;
using NUnit.Framework;

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
        public void MustCompleteTheTransaction()
        {
            using (var target = new TestableDependentTransactionAdapter(transaction.Object, true))
            {
                target.Commit();
            }

            transaction.Verify(o => o.Complete(), Times.Once);
        }
    }
}