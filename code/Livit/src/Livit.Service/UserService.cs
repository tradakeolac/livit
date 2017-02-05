namespace Livit.Service
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Data.Repositories;
    using System;
    using Exceptions;

    public class UserService : LivitServiceBase, IUserService
    {
        protected readonly IConfiguration Configuration;

        public UserService(IAsyncUnitOfWork unitOfWork, IAsyncDataLoader dataLoader,
            IConfiguration configuration)
            : base(unitOfWork, dataLoader)
        {
            this.Configuration = configuration;
        }

        public async Task<string> GetGrantToManageLeaveRequests()
        {
            string url = null;
            try
            {
                url = this.Configuration.GetValue<string>("LoginUrl");
            }
            catch (Exception ex)
            {
                throw new GetActionException("Can not find the section LoginUrl in configuration file");
            }

            return await Task.FromResult(url);
        }
    }
}