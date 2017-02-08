namespace Livit.Data.Repositories
{
    using Livit.Data.Specifications;
    using System.Linq;

    public interface IDataLoaderRepository
    {
        /// <summary>
        /// Finds entities based on provided criteria.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        IQueryable<TEntity> Find<TEntity>(ISpecification<TEntity> criteria) where TEntity : class;

        /// <summary>
        /// Finds one entity based on provided criteria.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        TEntity FindOne<TEntity>(ISpecification<TEntity> criteria) where TEntity : class;

        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById<TEntity>(object id) where TEntity : class;

        /// <summary>
        /// Get result with paging
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="criteria"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IQueryable<TEntity> Get<TEntity>(ISpecification<TEntity> criteria, int page, int pageSize) where TEntity : class;

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        IQueryable<TEntity> GetAll<TEntity>() where TEntity : class;

        /// <summary>
        /// Counts the specified entities.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        int Count<TEntity>() where TEntity : class;

        /// <summary>
        /// Counts entities satifying specification.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        int Count<TEntity>(ISpecification<TEntity> criteria) where TEntity : class;
    }
}