using System;
using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Extensions.EntityFrameworkCore.TestObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.Extensions.EntityFrameworkCore
{
    [TestFixture]
    public class DbContextAdapterTests
    {
        private Mock<DbContext> dbContext;
        private TestableDatabaseFacade database;

        private Mock<IDbContextTransaction> transaction;

        [SetUp]
        public void Setup()
        {
            dbContext = new Mock<DbContext>();

            database = new TestableDatabaseFacade(dbContext.Object);
            dbContext.Setup(o => o.Database).Returns(database);

            transaction = new Mock<IDbContextTransaction>();
        }

        [Test]
        public void ThrowAnExceptionWhenDbContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DbContextAdapter(null));
        }

        [Test]
        public void BeginsTheTransaction()
        {
            database.SetTransaction(transaction.Object);

            var target = new DbContextAdapter(dbContext.Object);
            var result = target.BeginTransaction();

            Assert.NotNull(result);
            Assert.IsInstanceOf<DbContextTransactionAdapter>(result);
        }

        [Test]
        public void ThrowAnExceptionWhenTheTransactionIsNull()
        {
            database.SetTransaction(null);

            var target = new DbContextAdapter(dbContext.Object);
            Assert.Throws<InvalidOperationException>(() => target.BeginTransaction());
        }

        [Test]
        public async Task BeginsTheTransactionAsynchronously()
        {
            database.SetTransaction(transaction.Object);

            var target = new DbContextAdapter(dbContext.Object);
            var result = await target.BeginTransactionAsync(CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsInstanceOf<DbContextTransactionAdapter>(result);
        }

        [Test]
        public void ThrowAnExceptionWhenTheTransactionIsNullAsynchronously()
        {
            database.SetTransaction(null);

            var target = new DbContextAdapter(dbContext.Object);
            Assert.ThrowsAsync<InvalidOperationException>(async () => await target.BeginTransactionAsync(CancellationToken.None));
        }
    }
}