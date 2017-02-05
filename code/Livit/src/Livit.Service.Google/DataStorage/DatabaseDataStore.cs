namespace Livit.Service.Google.DataStorage
{
    using System.Threading.Tasks;
    using Livit.Data.Repositories;
    using Livit.Model.Entities;
    using global::Google.Apis.Util.Store;

    public class DatabaseDataStore : IDataStore
    {
        protected readonly IAsyncDataLoader DataLoader;
        protected readonly IRepository Repository;
        protected readonly IAsyncUnitOfWork UnitOfWork;
        protected readonly IEntityFactory EntityFactory;

        public DatabaseDataStore(IAsyncUnitOfWork unitOfWork,
            IRepository repository, IAsyncDataLoader dataLoader,
            IEntityFactory entityFactory)
        {
            this.DataLoader = dataLoader;
            this.UnitOfWork = unitOfWork;
            this.Repository = repository;
            this.EntityFactory = entityFactory;
        }

        public Task ClearAsync()
        {
            return Task.FromResult<object>(null);
        }

        public async Task DeleteAsync<T>(string key)
        {
            var entity = await this.DataLoader.GetByIdAsync<TokenResponseEntity>(key);
            this.Repository.Delete(entity);
            await this.UnitOfWork.SaveChangeAsync();
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var entity = await this.DataLoader.GetByIdAsync<TokenResponseEntity>(key);
            return await Task.FromResult(this.EntityFactory.Create<T>(entity));
        }

        public async Task StoreAsync<T>(string key, T value)
        {
            var token = this.EntityFactory.Create<TokenResponseEntity>(value);

            var dbEntity = await this.DataLoader.GetByIdAsync<TokenResponseEntity>(key);

            if (dbEntity != null)
            {
                dbEntity.AccessToken = token.AccessToken;
                dbEntity.Issued = token.Issued;
                dbEntity.ExpiresInSeconds = token.ExpiresInSeconds;
                dbEntity.RefreshToken = token.RefreshToken;
                dbEntity.Scope = token.Scope;
                dbEntity.TokenType = token.TokenType;

                this.Repository.Update(dbEntity);
            }
            else
            {
                token.Id = key;
                this.Repository.Add(token);
            }

            await this.UnitOfWork.SaveChangeAsync();
        }
    }
}
