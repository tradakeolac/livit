namespace Livit.Service
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Data.Repositories;
    using System;
    using Exceptions;
    using System.Collections.Generic;
    using Infrastructure.Configurations;

    public class UserService : LivitServiceBase, IUserService
    {
        protected readonly ILivitConfiguration Configuration;

        public UserService(IAsyncUnitOfWork unitOfWork, IAsyncDataLoader dataLoader,
            ILivitConfiguration configuration)
            : base(unitOfWork, dataLoader)
        {
            this.Configuration = configuration;
        }

        public async Task<string> GetGrantToManageLeaveRequests()
        {
            string url = null;

            var parameters = new Dictionary<string, string>
            {
                { "response_type", "code" },
                { "client_id", Configuration.Secrets.ClientId },
                { "client_secret", Configuration.Secrets.ClientSecret },
                { "scope", "https://www.googleapis.com/auth/calendar" },
                { "redirect_uri", Configuration.Secrets.RedirectUri }
            };

            url = Configuration.Secrets.AuthenUri;

            return await Task.FromResult(url);
        }
    }
}