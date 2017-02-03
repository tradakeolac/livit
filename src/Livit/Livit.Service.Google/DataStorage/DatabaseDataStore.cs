using System;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

using Livit.Data;
using Livit.Data.Repositories;
using Livit.Data.Specifications;
using Livit.Model.Entities;
using System.IO;

namespace Livit.Service.Google.DataStorage
{    
    public class DatabaseDataStore : IDataStore
    {

        protected readonly IAsyncDataLoader DataLoader;
        protected readonly IRepository Repository;
        protected readonly IAsyncUnitOfWork UnitOfWork;

        public DatabaseDataStore(IAsyncUnitOfWork unitOfWork, IRepository repository, IAsyncDataLoader dataLoader)
        {
            this.DataLoader = dataLoader;
            this.UnitOfWork = unitOfWork;
            this.Repository = repository;
        }

        public Task ClearAsync()
        {
            return null;
        }

        public async Task DeleteAsync<T>(string key)
        {
            var entity = await this.DataLoader.GetByIdAsync<RequestedLeaveEntity>(key);
            
            await this.UnitOfWork.SaveChangeAsync();
        }

        public Task<T> GetAsync<T>(string key)
        {
            return Task.FromResult<T>(default(T));
        }

        public Task StoreAsync<T>(string key, T value)
        {
            return Task.FromResult<T>(default(T));
        }
    }
}
