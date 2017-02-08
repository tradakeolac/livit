namespace Livit.Data.Repositories
{
    using Livit.Data.Specifications;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAsyncDataLoader
    {
        /// <summary>
        /// Finds entities based on provided criteria.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> FindAsync<TEntity>(ISpecification<TEntity> criteria) where TEntity : class;

        /// <summary>
        /// Finds one entity based on provided criteria.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        Task<TEntity> FindOneAsync<TEntity>(ISpecification<TEntity> criteria) where TEntity : class;

        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync<TEntity>(object id) where TEntity : class;

        /// <summary>
        /// Get result with paging
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="criteria"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAsync<TEntity>(ISpecification<TEntity> criteria, int page, int pageSize) where TEntity : class;

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : class;

        /// <summary>
        /// Counts the specified entities.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        Task<int> CountAsync<TEntity>() where TEntity : class;

        /// <summary>
        /// Counts entities satifying specification.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        Task<int> CountAsync<TEntity>(ISpecification<TEntity> criteria) where TEntity : class;
    }
}