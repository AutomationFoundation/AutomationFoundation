using System;
using System.Data.SqlClient;
using AutomationFoundation.Extensions.SqlClient.Primitives;
using AutomationFoundation.Extensions.SystemData;

namespace AutomationFoundation.Extensions.SqlClient
{
    /// <summary>
    /// Provides an adapter for a <see cref="SqlTransaction"/> transaction.
    /// </summary>
    public sealed class SqlTransactionAdapter : DbTransactionAdapter<SqlTransaction>, ISqlTransactionAdapter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlTransactionAdapter"/> class.
        /// </summary>
        /// <param name="transaction">The transaction which is being adapted.</param>
        /// <param name="ownsTransaction">Optional. Identifies whether the adapter will take ownership of the transaction.</param>
        public SqlTransactionAdapter(SqlTransaction transaction, bool ownsTransaction = true) 
            : this(new SqlTransactionWrapper(transaction), ownsTransaction)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlTransactionAdapter"/> class.
        /// </summary>
        /// <param name="transaction">The transaction which is being adapted.</param>
        /// <param name="ownsTransaction">Optional. Identifies whether the adapter will take ownership of the transaction.</param>
        internal SqlTransactionAdapter(ISqlTransactionWrapper transaction, bool ownsTransaction = true)
            : base(transaction, ownsTransaction)
        {
        }

        /// <inheritdoc />
        public void Rollback(string savePointName)
        {
            if (string.IsNullOrWhiteSpace(savePointName))
            {
                throw new ArgumentNullException(nameof(savePointName));
            }

            GuardMustNotBeDisposed();

            GetSqlTransactionWrapper()
                .Rollback(savePointName);
        }

        /// <inheritdoc />
        public void Save(string savePointName)
        {
            if (string.IsNullOrWhiteSpace(savePointName))
            {
                throw new ArgumentNullException(nameof(savePointName));
            }

            GuardMustNotBeDisposed();

            GetSqlTransactionWrapper()
                .Save(savePointName);
        }

        private ISqlTransactionWrapper GetSqlTransactionWrapper()
        {
            return (ISqlTransactionWrapper)Transaction;
        }
    }
}
