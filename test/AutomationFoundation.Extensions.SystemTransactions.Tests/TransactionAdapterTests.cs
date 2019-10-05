using System;
using System.Transactions;
using AutomationFoundation.Extensions.SystemTransactions.Primitives;
using AutomationFoundation.Extensions.SystemTransactions.TestObjects;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.Extensions.SystemTransactions
{
    public class TransactionAdapterTests
    {
        private Mock<CommittableTransactionWrapper> transaction;

        [SetUp]
        public void Setup()
        {
            transaction = new Mock<CommittableTransactionWrapper>();
        }

        [Test]
        public void MustRollbackTheTransaction()
        {
            using (var target = new StubTransactionAdapter(transaction.Object))
            {
                target.Rollback();
            }

            transaction.Verify(o => o.Rollback(), Times.Once);
        }

        [Test]
        public void MustDisposeTheTransactionWhenOwned()
        {
            using (var target = new StubTransactionAdapter(transaction.Object))
            {
                Assert.True(target.OwnsTransaction);
            }

            transaction.Verify(o => o.Dispose(), Times.Once);
        }

        [Test]
        public void MustNotDisposeTheTransactionWhenNotOwned()
        {
            using (var target = new StubTransactionAdapter(transaction.Object, false))
            {
                Assert.False(target.OwnsTransaction);
            }

            transaction.Verify(o => o.Dispose(), Times.Never);
        }

        [Test]
        public void ThrowsExceptionWhenAlreadyDisposedOnRollback()
        {
            var target = new StubTransactionAdapter(transaction.Object);
            target.Dispose();

            Assert.Throws<ObjectDisposedException>(() => target.Rollback());
        }

        [Test]
        public void ThrowsAnExceptionWhenTheTransactionIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new StubTransactionAdapter(null));
        }
    }
}