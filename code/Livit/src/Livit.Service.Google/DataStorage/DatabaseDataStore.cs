namespace Livit.Service.Google.DataStorage
{
    using global::Google.Apis.Util.Store;
    using Infrastructure.Ultility;
    using Livit.Data.Repositories;
    using Livit.Model.Entities;
    using System;
    using System.Threading.Tasks;

    public class DatabaseDataStore : IDataStore
    {
        protected readonly IAsyncDataLoader DataLoader;
        protected readonly IRepository Repository;
        protected readonly IAsyncUnitOfWork UnitOfWork;
        protected readonly IEntityFactory EntityFactory;
        protected readonly IDateTimeAdapter DateTimeAdapter;

        public DatabaseDataStore(IAsyncUnitOfWork unitOfWork,
            IRepository repository, IAsyncDataLoader dataLoader,
            IEntityFactory entityFactory, IDateTimeAdapter dateTimeAdapter)
        {
            this.DataLoader = dataLoader;
            this.UnitOfWork = unitOfWork;
            this.Repository = repository;
            this.EntityFactory = entityFactory;
            this.DateTimeAdapter = dateTimeAdapter;
        }

        public Task ClearAsync()
        {
            return Task.FromResult<object>(null);
        }

        public async Task DeleteAsync<T>(string key)
        {
            var entity = await DataLoader.GetByIdAsync<TokenResponseEntity>(key);
            this.Repository.Delete(entity);
            await this.UnitOfWork.SaveChangeAsync();
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var entity = await this.DataLoader.GetByIdAsync<TokenResponseEntity>(key);
            return entity == null ? default(T) : await Task.FromResult(this.EntityFactory.Create<T>(entity));
        }

        public async Task StoreAsync<T>(string key, T value)
        {
            var token = this.EntityFactory.Create<TokenResponseEntity>(value);

            if (token.Issued == this.DateTimeAdapter.Min)
                token.Issued = this.DateTimeAdapter.Now;

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