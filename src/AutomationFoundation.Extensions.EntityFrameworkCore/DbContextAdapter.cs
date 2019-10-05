using System;
using System.Threading;
using System.Threading.Tasks;
using AutomationFoundation.Transactions.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AutomationFoundation.Extensions.EntityFrameworkCore
{
    /// <summary>
    /// Provides an adapter for a <see cref="DbContext"/> for use within the framework.
    /// </summary>
    public class DbContextAdapter : ISupportTransaction
    {
        private readonly DbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextAdapter"/> class.
        /// </summary>
        /// <param name="dbContext">The database context being adapted.</param>
        public DbContextAdapter(DbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <inheritdoc />
        public ITransaction BeginTransaction()
        {
            IDbContextTransaction transaction = null;

            try
            {
                transaction = dbContext.Database.BeginTransaction();
                if (transaction == null)
                {
                    OnTransactionNotCreated();
                }

                return CreateAdapter(transaction);
            }
            catch (Exception)
            {
                transaction?.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Creates an adapter for the transaction.
        /// </summary>
        /// <param name="underlyingTransaction">The underlying transaction which is being adapted.</param>
        /// <returns>The adapter instance.</returns>
        protected virtual DbContextTransactionAdapter CreateAdapter(IDbContextTransaction underlyingTransaction)
        {
            return new DbContextTransactionAdapter(underlyingTransaction);
        }

        /// <summary>
        /// Occurs when the transaction has not been created.
        /// </summary>
        protected virtual void OnTransactionNotCreated()
        {
            throw new InvalidOperationException("The transaction was not started.");
        }

        /// <inheritdoc />
        public async Task<ITransaction> BeginTransactionAsync(CancellationToken cancellationToken)
        {
            IDbContextTransaction transaction = null;

            try
            {
                transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
                if (transaction == null)
                {
                    OnTransactionNotCreated();
                }

                return CreateAdapter(transaction);
            }
            catch (Exception)
            {
                transaction?.Dispose();
                throw;
            }
        }
    }
}