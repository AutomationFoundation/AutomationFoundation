using System;
using System.Transactions;
using AutomationFoundation.Extensions.SystemTransactions.Primitives;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.Extensions.SystemTransactions
{
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
            using (var target = new CommittableTransactionAdapter(transaction.Object))
            {
                target.Commit();
            }

            transaction.Verify(o => o.Commit(), Times.Once);
        }

        [Test]
        public void MustRollbackTheTransaction()
        {
            using (var target = new CommittableTransactionAdapter(transaction.Object))
            {
                target.Rollback();
            }

            transaction.Verify(o => o.Rollback(), Times.Once);
        }

        [Test]
        public void MustDisposeTheTransactionWhenOwned()
        {
            using (var target = new CommittableTransactionAdapter(transaction.Object))
            {
                Assert.True(target.OwnsTransaction);
            }

            transaction.Verify(o => o.Dispose(), Times.Once);
        }

        [Test]
        public void MustNotDisposeTheTransactionWhenNotOwned()
        {
            using (var target = new CommittableTransactionAdapter(transaction.Object, false))
            {
                Assert.False(target.OwnsTransaction);
            }

            transaction.Verify(o => o.Dispose(), Times.Never);
        }

        [Test]
        public void ThrowsExceptionWhenAlreadyDisposedOnCommit()
        {
            var target = new CommittableTransactionAdapter(transaction.Object);
            target.Dispose();

            Assert.Throws<ObjectDisposedException>(() => target.Commit());
        }

        [Test]
        public void ThrowsExceptionWhenAlreadyDisposedOnRollback()
        {
            var target = new CommittableTransactionAdapter(transaction.Object);
            target.Dispose();

            Assert.Throws<ObjectDisposedException>(() => target.Rollback());
        }

        [Test]
        public void ReturnsTheUnderlyingTransactionAsExpected()
        {
            var expected = new CommittableTransaction();
            transaction.Setup(o => o.UnderlyingTransaction).Returns(expected);

            using (var target = new CommittableTransactionAdapter(transaction.Object))
            {
                Assert.AreEqual(expected, target.UnderlyingTransaction);
            }
        }

        [Test]
        public void ReturnsTheUnderlyingTransactionAsExpectedWhenUsingRealObject()
        {
            var expected = new CommittableTransaction();

            using (var target = new CommittableTransactionAdapter(expected))
            {
                Assert.AreEqual(expected, target.UnderlyingTransaction);
            }
        }

        [Test]
        public void ThrowsAnExceptionWhenTheTransactionIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new CommittableTransactionAdapter((CommittableTransaction)null));
        }
    }
}