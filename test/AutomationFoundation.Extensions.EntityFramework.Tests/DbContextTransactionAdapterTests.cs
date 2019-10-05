using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Extensions.EntityFramework.Primitives;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.Extensions.EntityFramework
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
        public async Task MustCommitTheTransaction()
        {
            using (var target = new DbContextTransactionAdapter(transaction.Object))
            {
                await target.CommitAsync(CancellationToken.None);
            }

            transaction.Verify(o => o.Commit(), Times.Once);
        }

        [Test]
        public async Task MustRollbackTheTransaction()
        {
            using (var target = new DbContextTransactionAdapter(transaction.Object))
            {
                await target.RollbackAsync(CancellationToken.None);
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

            Assert.ThrowsAsync<ObjectDisposedException>(async () => await target.CommitAsync(CancellationToken.None));
        }

        [Test]
        public void ThrowsExceptionWhenAlreadyDisposedOnRollback()
        {
            var target = new DbContextTransactionAdapter(transaction.Object);
            target.Dispose();

            Assert.ThrowsAsync<ObjectDisposedException>(async () => await target.RollbackAsync(CancellationToken.None));
        }

        [Test]
        public void ThrowsAnExceptionWhenTheTransactionIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DbContextTransactionAdapter((DbContextTransaction)null));
        }
    }
}