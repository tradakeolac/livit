namespace Livit.Data.EntityFramework
{
    using Livit.Data.Repositories;
    using Livit.Data.Specifications;
    using Microsoft.EntityFrameworkCore;

    public class EFCoreRepository : EFCoreDataLoader, IRepository
    {
        public EFCoreRepository(LivitDbContext dbContext) : base(dbContext)
        {
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            DbSet<TEntity>().Add(entity);
        }

        public void Delete<TEntity>(ISpecification<TEntity> criteria) where TEntity : class
        {
            var entities = this.Find(criteria);
            DbSet<TEntity>().RemoveRange(entities);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            DbSet<TEntity>().Remove(entity);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            DbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
