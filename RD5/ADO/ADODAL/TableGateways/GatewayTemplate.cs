using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using ADODAL.Infrastructure;

namespace ADODAL.TableGateways
{
    /// <summary>
    /// Base for all TableGateways, which using one pool of SQL-connections.
    /// Template methods: Add, GetByKey, GetAll, Update, DeleteByKey, Delete.
    /// Implements 'Template method' design pattern.
    /// </summary>
    /// <typeparam name="TEntity">Entity, represented in the table</typeparam>
    /// <typeparam name="TKey">Primary key type of the table</typeparam>
    public abstract class GatewayTemplate<TEntity, TKey> : IDisposable
        where TEntity: class
    {
        private bool disposed = false;
        protected string connectionStrings;

        protected SqlConnection connection;
        protected SqlTransaction transaction;
        protected SqlCommand command;
        protected SqlDataReader reader;


        public GatewayTemplate(string connectionName)
        {
            try
            {
                connectionStrings = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
                connection = new SqlConnection(connectionStrings);
                connection.Open();
                transaction = connection.BeginTransaction();
                command = connection.CreateCommand();
                command.Transaction = transaction;
            }
            catch (NullReferenceException exception)
            {
                throw new TableGatewayException($"Failed to get connection strings due to: {exception.Message}");
            }
            catch (SqlException exception)
            {
                connection.Close();
                throw new TableGatewayException($"Failed to create connection due to: {exception.Message}");
            }
        }

        // All template methods are working with Transaction-like queries
        // The common example for every method is:
        // command.CommandText = "<SQL-command>";
        // command.Execute...();
        // ...other code...

        /// <summary>
        /// Template method.
        /// Inserts new rows in table, EXCEPT IDENTITY AND SQL AUTO-CALCULATION ROWS.
        /// </summary>
        /// <param name="entity"></param>
        public abstract void Add(TEntity entity);

        /// <summary>
        /// Template method.
        /// Returns a single instance of entity by given ID (returns NULL if there's no row with given ID).
        /// </summary>
        /// <param name="key">Primary key type of the table</param>
        public abstract TEntity GetByKey(TKey key);

        /// <summary>
        /// Template method.
        /// Returns all rows in a table.
        /// </summary>
        /// <param name="key">Primary key type of the table</param>
        public abstract IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Template method.
        /// Sets NEW values for rows WITH THE SAME ID as in param.
        /// </summary>
        /// <param name="entity"></param>
        public abstract void Update(TEntity entity);

        /// <summary>
        /// Template method.
        /// Removes a single row with same properties of given param.
        /// </summary>
        /// <param name="entity">Primary key type of the table</param>
        public abstract void DeleteByKey(TKey key);

        /// <summary>
        /// Template method.
        /// Removes a single row with a same ID.
        /// </summary>
        /// <param name="key">Primary key type of the table</param>
        public abstract void Delete(TEntity entity);

        /// <summary>
        /// Commits the main transaction.
        /// </summary>
        public virtual void SaveChanges()
        {
            try
            { transaction.Commit(); }
            catch (Exception exception)
            {
                transaction.Rollback();
                throw new TableGatewayException($"Failed to save changes to the database due to: {exception.Message}");
            }
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (reader != null)
                        reader.Close();

                    connection.Close();
                }

                disposed = true;
            }
        }

        /// <summary>
        /// Closes DB connection and releases all unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
