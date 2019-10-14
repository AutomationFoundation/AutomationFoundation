using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
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
        public async Task MustCommitTheTransaction()
        {
            using var target = new DbTransactionAdapter<DbTransaction>(transaction.Object);
            await target.CommitAsync(CancellationToken.None);

            transaction.Verify(o => o.Commit(), Times.Once);
        }

        [Test]
        public async Task MustRollbackTheTransaction()
        {
            using (var target = new DbTransactionAdapter<DbTransaction>(transaction.Object))
            {
                await target.RollbackAsync(CancellationToken.None);
            }

            transaction.Verify(o => o.Rollback(), Times.Once);
        }

        [Test]
        public void ThrowsExceptionWhenAlreadyDisposedOnCommit()
        {
            var target = new DbTransactionAdapter<DbTransaction>(transaction.Object);
            target.Dispose();

            Assert.ThrowsAsync<ObjectDisposedException>(async () => await target.CommitAsync(CancellationToken.None));
        }

        [Test]
        public void ThrowsExceptionWhenAlreadyDisposedOnRollback()
        {
            var target = new DbTransactionAdapter<DbTransaction>(transaction.Object);
            target.Dispose();

            Assert.ThrowsAsync<ObjectDisposedException>(async () => await target.RollbackAsync(CancellationToken.None));
        }

        [Test]
        public void ReturnsTheUnderlyingTransactionAsExpected()
        {
            using var target = new DbTransactionAdapter<DbTransaction>(transaction.Object);
            Assert.AreEqual(transaction.Object, target.UnderlyingTransaction);
        }

        [Test]
        public void ThrowsAnExceptionWhenTheTransactionIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DbTransactionAdapter<DbTransaction>(null));
        }
    }

}
