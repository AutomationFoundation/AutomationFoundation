using System;
using AutomationFoundation.Extensions.SqlClient.Primitives;
using Moq;
using NUnit.Framework;

/* Unmerged change from project 'AutomationFoundation.Extensions.SqlClient.Tests(net472)'
Before:
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
            using var target = new SqlTransactionAdapter(transaction.Object);
            Assert.Throws<ArgumentNullException>(() => target.Rollback(null));
        }

        [Test]
        public void ThrowAnExceptionWhenSavePointNameIsEmptyOnRollback()
        {
            using var target = new SqlTransactionAdapter(transaction.Object);
            Assert.Throws<ArgumentNullException>(() => target.Rollback(""));
        }

        [Test]
        public void ThrowAnExceptionWhenSavePointNameIsWhiteSpaceOnRollback()
        {
            using var target = new SqlTransactionAdapter(transaction.Object);
            Assert.Throws<ArgumentNullException>(() => target.Rollback("      "));
        }


        [Test]
        public void ShouldRollbackToSavePointName()
        {
            var savePointName = "MySavePointName";

            using var target = new SqlTransactionAdapter(transaction.Object);
            target.Rollback(savePointName);

            transaction.Verify(o => o.Rollback(savePointName));
        }
        
        [Test]
        public void ShouldSaveToSavePointName()
        {
            var savePointName = "MySavePointName";

            using var target = new SqlTransactionAdapter(transaction.Object);
            target.Save(savePointName);

            transaction.Verify(o => o.Save(savePointName));
        }

        [Test]
        public void ThrowAnExceptionWhenSavePointNameIsNullOnSave()
        {
            using var target = new SqlTransactionAdapter(transaction.Object);
            Assert.Throws<ArgumentNullException>(() => target.Save(null));
        }

        [Test]
        public void ThrowAnExceptionWhenSavePointNameIsEmptyOnSave()
        {
            using var target = new SqlTransactionAdapter(transaction.Object);
            Assert.Throws<ArgumentNullException>(() => target.Save(""));
        }

        [Test]
        public void ThrowAnExceptionWhenSavePointNameIsWhiteSpaceOnSave()
        {
            using var target = new SqlTransactionAdapter(transaction.Object);
            Assert.Throws<ArgumentNullException>(() => target.Save("      "));
        }
After:
namespace AutomationFoundation.Extensions.SqlClient;

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
        using var target = new SqlTransactionAdapter(transaction.Object);
        Assert.Throws<ArgumentNullException>(() => target.Rollback(null));
    }

    [Test]
    public void ThrowAnExceptionWhenSavePointNameIsEmptyOnRollback()
    {
        using var target = new SqlTransactionAdapter(transaction.Object);
        Assert.Throws<ArgumentNullException>(() => target.Rollback(""));
    }

    [Test]
    public void ThrowAnExceptionWhenSavePointNameIsWhiteSpaceOnRollback()
    {
        using var target = new SqlTransactionAdapter(transaction.Object);
        Assert.Throws<ArgumentNullException>(() => target.Rollback("      "));
    }


    [Test]
    public void ShouldRollbackToSavePointName()
    {
        var savePointName = "MySavePointName";

        using var target = new SqlTransactionAdapter(transaction.Object);
        target.Rollback(savePointName);

        transaction.Verify(o => o.Rollback(savePointName));
    }
    
    [Test]
    public void ShouldSaveToSavePointName()
    {
        var savePointName = "MySavePointName";

        using var target = new SqlTransactionAdapter(transaction.Object);
        target.Save(savePointName);

        transaction.Verify(o => o.Save(savePointName));
    }

    [Test]
    public void ThrowAnExceptionWhenSavePointNameIsNullOnSave()
    {
        using var target = new SqlTransactionAdapter(transaction.Object);
        Assert.Throws<ArgumentNullException>(() => target.Save(null));
    }

    [Test]
    public void ThrowAnExceptionWhenSavePointNameIsEmptyOnSave()
    {
        using var target = new SqlTransactionAdapter(transaction.Object);
        Assert.Throws<ArgumentNullException>(() => target.Save(""));
    }

    [Test]
    public void ThrowAnExceptionWhenSavePointNameIsWhiteSpaceOnSave()
    {
        using var target = new SqlTransactionAdapter(transaction.Object);
        Assert.Throws<ArgumentNullException>(() => target.Save("      "));
*/

namespace AutomationFoundation.Extensions.SqlClient;

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
        using var target = new SqlTransactionAdapter(transaction.Object);
        Assert.Throws<ArgumentNullException>(() => target.Rollback(null));
    }

    [Test]
    public void ThrowAnExceptionWhenSavePointNameIsEmptyOnRollback()
    {
        using var target = new SqlTransactionAdapter(transaction.Object);
        Assert.Throws<ArgumentNullException>(() => target.Rollback(""));
    }

    [Test]
    public void ThrowAnExceptionWhenSavePointNameIsWhiteSpaceOnRollback()
    {
        using var target = new SqlTransactionAdapter(transaction.Object);
        Assert.Throws<ArgumentNullException>(() => target.Rollback("      "));
    }


    [Test]
    public void ShouldRollbackToSavePointName()
    {
        var savePointName = "MySavePointName";

        using var target = new SqlTransactionAdapter(transaction.Object);
        target.Rollback(savePointName);

        transaction.Verify(o => o.Rollback(savePointName));
    }

    [Test]
    public void ShouldSaveToSavePointName()
    {
        var savePointName = "MySavePointName";

        using var target = new SqlTransactionAdapter(transaction.Object);
        target.Save(savePointName);

        transaction.Verify(o => o.Save(savePointName));
    }

    [Test]
    public void ThrowAnExceptionWhenSavePointNameIsNullOnSave()
    {
        using var target = new SqlTransactionAdapter(transaction.Object);
        Assert.Throws<ArgumentNullException>(() => target.Save(null));
    }

    [Test]
    public void ThrowAnExceptionWhenSavePointNameIsEmptyOnSave()
    {
        using var target = new SqlTransactionAdapter(transaction.Object);
        Assert.Throws<ArgumentNullException>(() => target.Save(""));
    }

    [Test]
    public void ThrowAnExceptionWhenSavePointNameIsWhiteSpaceOnSave()
    {
        using var target = new SqlTransactionAdapter(transaction.Object);
        Assert.Throws<ArgumentNullException>(() => target.Save("      "));
    }
}