namespace Livit.Data.EntityFramework
{
    using System;
    using System.Threading.Tasks;
    using Livit.Data.Repositories.Abstractions;
    using Microsoft.EntityFrameworkCore;

    /// <summary> 
    /// The Entity Framework implementation of IUnitOfWork 
    /// </summary> 
    public sealed class EFCoreUnitOfWork : IUnitOfWork, IAsyncUnitOfWork
    {
        /// <summary> 
        /// The DbContext 
        /// </summary> 
        private DbContext _dbContext;

        /// <summary> 
        /// Initializes a new instance of the UnitOfWork class. 
        /// </summary> 
        /// <param name="context">The object context</param> 
        public EFCoreUnitOfWork(DbContext context)
        {

            _dbContext = context;
        }

        /// <summary>
        /// Check current in transaction
        /// </summary>
        public bool IsInTransaction
        {
            get
            {
                return _dbContext.Database.CurrentTransaction != null;
            }
        }

        /// <summary>
        /// Begin new transaction
        /// </summary>
        public void BeginTransaction()
        {
            if (_dbContext.Database.CurrentTransaction != null)
                _dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (_dbContext.Database.CurrentTransaction != null)
                _dbContext.Database.CurrentTransaction.Commit();
        }

        /// <summary> 
        /// Disposes the current object 
        /// </summary> 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Roll back current transaction
        /// </summary>
        public void RollBackTransaction()
        {
            // Save changes with the default options 
            _dbContext.Database.CurrentTransaction.Rollback();
        }

        public async Task SaveChangeAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Save all changes
        /// </summary>
        public void SaveChanges()
        {
            // Save changes with the default options 
            _dbContext.SaveChanges();
        }

        /// <summary> 
        /// Disposes all external resources. 
        /// </summary> 
        /// <param name="disposing">The dispose indicator.</param> 
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                    _dbContext = null;
                }
            }
        }
    }
}
