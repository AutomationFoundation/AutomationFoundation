using System;
using System.Data.Common;


/* Unmerged change from project 'AutomationFoundation.Extensions.SystemData(net472)'
Before:
namespace AutomationFoundation.Extensions.SystemData.Primitives
{
    /// <summary>
    /// INFRASTRUCTURE ONLY: This class is not intended for use within your application code, use at your own risk.
    /// </summary>
    /// <typeparam name="TTransaction">The type of transaction being wrapped.</typeparam>
    public class DbTransactionWrapper<TTransaction> : IDbTransactionWrapper<TTransaction> 
        where TTransaction : DbTransaction
    {
        /// <inheritdoc />
        public TTransaction UnderlyingTransaction { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbTransactionWrapper{TTransaction}"/> class.
        /// </summary>
        /// <param name="transaction">The transaction being wrapped.</param>
        public DbTransactionWrapper(TTransaction transaction)
        {
            UnderlyingTransaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="DbTransactionWrapper{TTransaction}"/> class.
        /// </summary>
        ~DbTransactionWrapper()
        {
            Dispose(false);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources, otherwise false to release unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnderlyingTransaction.Dispose();
            }
        }

        /// <inheritdoc />
        public void Commit()
        {
            UnderlyingTransaction.Commit();
        }

        /// <inheritdoc />
        public void Rollback()
        {
            UnderlyingTransaction.Rollback();
        }
After:
namespace AutomationFoundation.Extensions.SystemData.Primitives;

/// <summary>
/// INFRASTRUCTURE ONLY: This class is not intended for use within your application code, use at your own risk.
/// </summary>
/// <typeparam name="TTransaction">The type of transaction being wrapped.</typeparam>
public class DbTransactionWrapper<TTransaction> : IDbTransactionWrapper<TTransaction> 
    where TTransaction : DbTransaction
{
    /// <inheritdoc />
    public TTransaction UnderlyingTransaction { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DbTransactionWrapper{TTransaction}"/> class.
    /// </summary>
    /// <param name="transaction">The transaction being wrapped.</param>
    public DbTransactionWrapper(TTransaction transaction)
    {
        UnderlyingTransaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="DbTransactionWrapper{TTransaction}"/> class.
    /// </summary>
    ~DbTransactionWrapper()
    {
        Dispose(false);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources, otherwise false to release unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            UnderlyingTransaction.Dispose();
        }
    }

    /// <inheritdoc />
    public void Commit()
    {
        UnderlyingTransaction.Commit();
    }

    /// <inheritdoc />
    public void Rollback()
    {
        UnderlyingTransaction.Rollback();
*/
namespace AutomationFoundation.Extensions.SystemData.Primitives;

/// <summary>
/// INFRASTRUCTURE ONLY: This class is not intended for use within your application code, use at your own risk.
/// </summary>
/// <typeparam name="TTransaction">The type of transaction being wrapped.</typeparam>
public class DbTransactionWrapper<TTransaction> : IDbTransactionWrapper<TTransaction>
    where TTransaction : DbTransaction
{
    /// <inheritdoc />
    public TTransaction UnderlyingTransaction { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DbTransactionWrapper{TTransaction}"/> class.
    /// </summary>
    /// <param name="transaction">The transaction being wrapped.</param>
    public DbTransactionWrapper(TTransaction transaction)
    {
        UnderlyingTransaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="DbTransactionWrapper{TTransaction}"/> class.
    /// </summary>
    ~DbTransactionWrapper()
    {
        Dispose(false);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources, otherwise false to release unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            UnderlyingTransaction.Dispose();
        }
    }

    /// <inheritdoc />
    public void Commit()
    {
        UnderlyingTransaction.Commit();
    }

    /// <inheritdoc />
    public void Rollback()
    {
        UnderlyingTransaction.Rollback();
    }
}