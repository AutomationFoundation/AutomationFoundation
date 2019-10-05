using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace AutomationFoundation.Extensions.EntityFrameworkCore.TestObjects
{
    public class TestableDatabaseFacade : DatabaseFacade
    {
        public TestableDatabaseFacade(DbContext context) 
            : base(context)
        {
        }

        private IDbContextTransaction transaction;

        public void SetTransaction(IDbContextTransaction transaction)
        {
            this.transaction = transaction;
        }

        public override IDbContextTransaction BeginTransaction()
        {
            ThrowExceptionOnBeginTransaction();

            return transaction;
        }

        public override Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ThrowExceptionOnBeginTransaction();

            return Task.FromResult(transaction);
        }

        public bool IsThrowExceptionOnBeginTransactionEnabled { get; set; }

        public void ThrowExceptionOnBeginTransaction()
        {
            if (!IsThrowExceptionOnBeginTransactionEnabled)
            {
                return;
            }

            throw new Exception("This is a test exception!");
        }
    }
}