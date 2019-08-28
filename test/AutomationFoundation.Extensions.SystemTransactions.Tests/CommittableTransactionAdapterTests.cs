using AutomationFoundation.Extensions.SystemTransactions.Primitives;
using AutomationFoundation.Extensions.SystemTransactions.Stubs;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.Extensions.SystemTransactions
{
    [TestFixture]
    public class CommittableTransactionAdapterTests
    {
        private Mock<CommittableTransactionWrapper> transaction;

        [SetUp]
        public void Setup()
        {
            transaction = new Mock<CommittableTransactionWrapper>();
        }

        [Test]
        public void MustCommitTheTransaction()
        {
            using (var target = new TestableCommittableTransactionAdapter(transaction.Object, true))
            {
                target.Commit();
            }

            transaction.Verify(o => o.Commit(), Times.Once);
        }
    }
}