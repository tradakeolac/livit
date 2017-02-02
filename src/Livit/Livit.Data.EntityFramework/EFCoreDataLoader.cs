namespace Livit.Data.EntityFramework
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Livit.Data.Repositories.Abstractions;
    using Livit.Data.Specifications;
    using Microsoft.EntityFrameworkCore;

    public class EFCoreDataLoader : IDataLoaderRepository, IAsyncDataLoader
    {
        protected LivitDbContext DbContext;
        protected DbSet<TEntity> DbSet<TEntity>() where TEntity : class
        {
            return DbContext.Set<TEntity>();
        }
        
        public EFCoreDataLoader(LivitDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public int Count<TEntity>() where TEntity : class
        {
            return this.DbSet<TEntity>().Count();
        }

        public int Count<TEntity>(ISpecification<TEntity> criteria) where TEntity : class
        {
            return this.DbSet<TEntity>().AsQueryable().Count(criteria.ToExpression());
        }

        public IQueryable<TEntity> Find<TEntity>(ISpecification<TEntity> criteria) where TEntity : class
        {
            return this.DbSet<TEntity>().Where(criteria.ToExpression());
        }

        public TEntity FindOne<TEntity>(ISpecification<TEntity> criteria) where TEntity : class
        {
            return this.DbSet<TEntity>().FirstOrDefault(criteria.ToExpression());
        }

        public IQueryable<TEntity> Get<TEntity>(ISpecification<TEntity> criteria, int page, int pageSize) where TEntity : class
        {
            return this.DbSet<TEntity>().Where(criteria.ToExpression()).Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return DbSet<TEntity>().AsQueryable();
        }

        public TEntity GetById<TEntity>(object id) where TEntity : class
        {
            return DbSet<TEntity>().Find(id);
        }
        
        public async Task<IEnumerable<TEntity>> FindAsync<TEntity>(ISpecification<TEntity> criteria) where TEntity : class
        {
            return await DbSet<TEntity>().Where(criteria.ToExpression()).ToListAsync();
        }

        public async Task<TEntity> FindOneAsync<TEntity>(ISpecification<TEntity> criteria) where TEntity : class
        {
            return await DbSet<TEntity>().FirstOrDefaultAsync(criteria.ToExpression());
        }

        public async Task<TEntity> GetByIdAsync<TEntity>(object id) where TEntity : class
        {
            return await DbSet<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAsync<TEntity>(ISpecification<TEntity> criteria, int page, int pageSize) where TEntity : class
        {
            return await this.DbSet<TEntity>().Where(criteria.ToExpression()).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }
        
        public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : class
        {
            return await DbSet<TEntity>().ToListAsync();
        }

        public async Task<int> CountAsync<TEntity>() where TEntity : class
        {
            return await DbSet<TEntity>().CountAsync();
        }

        public async Task<int> CountAsync<TEntity>(ISpecification<TEntity> criteria) where TEntity : class
        {
            return await DbSet<TEntity>().CountAsync(criteria.ToExpression());
        }
    }
}
