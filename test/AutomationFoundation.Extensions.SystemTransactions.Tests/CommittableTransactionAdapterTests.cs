﻿using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Extensions.SystemTransactions.Primitives;
using AutomationFoundation.Extensions.SystemTransactions.TestObjects;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.Extensions.SystemTransactions;

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
    public async Task MustCommitTheTransaction()
    {
        using (var target = new TestableCommittableTransactionAdapter(transaction.Object, true))
        {
            await target.CommitAsync(CancellationToken.None);
        }

        transaction.Verify(o => o.Commit(), Times.Once);
    }
}