using System;
using AutomationFoundation.Extensions.SqlClient.Primitives;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.Extensions.SqlClient
{
    [TestFixture]
    public class SqlTransactionAdapterTests
    {
        private Mock<ISqlTransactionWrapper> transaction;

        [SetUp]
        public void Setup()
        {
            transaction = new Mock<ISqlTransactionWrapper>();
        }

        [Test]
        public void ThrowAnExceptionWhenSavePointNameIsNullOnRollback()
        {
            var target = new SqlTransactionAdapter(transaction.Object);
            Assert.Throws<ArgumentNullException>(() => target.Rollback(null));
        }

        [Test]
        public void ThrowAnExceptionWhenSavePointNameIsEmptyOnRollback()
        {
            var target = new SqlTransactionAdapter(transaction.Object);
            Assert.Throws<ArgumentNullException>(() => target.Rollback(""));
        }

        [Test]
        public void ThrowAnExceptionWhenSavePointNameIsWhiteSpaceOnRollback()
        {
            var target = new SqlTransactionAdapter(transaction.Object);
            Assert.Throws<ArgumentNullException>(() => target.Rollback("      "));
        }


        [Test]
        public void ShouldRollbackToSavePointName()
        {
            var savePointName = "MySavePointName";

            var target = new SqlTransactionAdapter(transaction.Object);
            target.Rollback(savePointName);

            transaction.Verify(o => o.Rollback(savePointName));
        }
        
        [Test]
        public void ShouldSaveToSavePointName()
        {
            var savePointName = "MySavePointName";

            var target = new SqlTransactionAdapter(transaction.Object);
            target.Save(savePointName);

            transaction.Verify(o => o.Save(savePointName));
        }

        [Test]
        public void ThrowAnExceptionWhenSavePointNameIsNullOnSave()
        {
            var target = new SqlTransactionAdapter(transaction.Object);
            Assert.Throws<ArgumentNullException>(() => target.Save(null));
        }

        [Test]
        public void ThrowAnExceptionWhenSavePointNameIsEmptyOnSave()
        {
            var target = new SqlTransactionAdapter(transaction.Object);
            Assert.Throws<ArgumentNullException>(() => target.Save(""));
        }

        [Test]
        public void ThrowAnExceptionWhenSavePointNameIsWhiteSpaceOnSave()
        {
            var target = new SqlTransactionAdapter(transaction.Object);
            Assert.Throws<ArgumentNullException>(() => target.Save("      "));
        }
    }
}