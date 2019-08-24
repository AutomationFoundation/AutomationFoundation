using System;
using System.Data.Common;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.Extensions.SystemData
{
    [TestFixture]
    public class DbTransactionAdapterTests
    {
        private Mock<DbTransaction> transaction;

        [SetUp]
        public void Setup()
        {
            transaction = new Mock<DbTransaction>();
        }

        [Test]
        public void MustCommitTheTransaction()
        {
            using (var target = new DbTransactionAdapter<DbTransaction>(transaction.Object))
            {
                target.Commit();
            }

            transaction.Verify(o => o.Commit(), Times.Once);
        }

        [Test]
        public void MustRollbackTheTransaction()
        {
            using (var target = new DbTransactionAdapter<DbTransaction>(transaction.Object))
            {
                target.Rollback();
            }

            transaction.Verify(o => o.Rollback(), Times.Once);
        }

        [Test]
        public void ThrowsExceptionWhenAlreadyDisposedOnCommit()
        {
            var target = new DbTransactionAdapter<DbTransaction>(transaction.Object);
            target.Dispose();

            Assert.Throws<ObjectDisposedException>(() => target.Commit());
        }

        [Test]
        public void ThrowsExceptionWhenAlreadyDisposedOnRollback()
        {
            var target = new DbTransactionAdapter<DbTransaction>(transaction.Object);
            target.Dispose();

            Assert.Throws<ObjectDisposedException>(() => target.Rollback());
        }

        [Test]
        public void ReturnsTheUnderlyingTransactionAsExpected()
        {
            using (var target = new DbTransactionAdapter<DbTransaction>(transaction.Object))
            {
                Assert.AreEqual(transaction.Object, target.UnderlyingTransaction);
            }
        }

        [Test]
        public void ThrowsAnExceptionWhenTheTransactionIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DbTransactionAdapter<DbTransaction>(null));
        }
    }

}
