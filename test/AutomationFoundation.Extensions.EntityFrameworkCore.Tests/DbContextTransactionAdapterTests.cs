using System;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.Extensions.EntityFrameworkCore
{
    [TestFixture]
    public class DbContextTransactionAdapterTests
    {
        private Mock<IDbContextTransaction> transaction;

        [SetUp]
        public void Setup()
        {
            transaction = new Mock<IDbContextTransaction>();
        }

        [Test]
        public void MustCommitTheTransaction()
        {
            using (var target = new DbContextTransactionAdapter(transaction.Object))
            {
                target.Commit();
            }

            transaction.Verify(o => o.Commit(), Times.Once);
        }

        [Test]
        public void MustRollbackTheTransaction()
        {
            using (var target = new DbContextTransactionAdapter(transaction.Object))
            {
                target.Rollback();
            }

            transaction.Verify(o => o.Rollback(), Times.Once);
        }

        [Test]
        public void MustDisposeTheTransactionWhenOwned()
        {
            using (var target = new DbContextTransactionAdapter(transaction.Object))
            {
                Assert.True(target.OwnsTransaction);
            }

            transaction.Verify(o => o.Dispose(), Times.Once);
        }

        [Test]
        public void MustNotDisposeTheTransactionWhenNotOwned()
        {
            using (var target = new DbContextTransactionAdapter(transaction.Object, false))
            {
                Assert.False(target.OwnsTransaction);
            }

            transaction.Verify(o => o.Dispose(), Times.Never);
        }

        [Test]
        public void ThrowsExceptionWhenAlreadyDisposedOnCommit()
        {
            var target = new DbContextTransactionAdapter(transaction.Object);
            target.Dispose();

            Assert.Throws<ObjectDisposedException>(() => target.Commit());
        }

        [Test]
        public void ThrowsExceptionWhenAlreadyDisposedOnRollback()
        {
            var target = new DbContextTransactionAdapter(transaction.Object);
            target.Dispose();

            Assert.Throws<ObjectDisposedException>(() => target.Rollback());
        }

        [Test]
        public void ThrowsAnExceptionWhenTheTransactionIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DbContextTransactionAdapter(null));
        }
    }
}