namespace Livit.Service
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Data.Repositories;
    using System;
    using Exceptions;
    using System.Collections.Generic;
    using Infrastructure.Configurations;

    public abstract class AuthenticationServiceBase : LivitServiceBase, IAuthenticationService
    {
        protected readonly ILivitConfiguration Configuration;

        public AuthenticationServiceBase(IAsyncUnitOfWork unitOfWork, IAsyncDataLoader dataLoader,
            ILivitConfiguration configuration)
            : base(unitOfWork, dataLoader)
        {
            this.Configuration = configuration;
        }

        public abstract Task<bool> Authorize(string authorizationCode);
        public abstract Task<string> GetGrantToManageLeaveRequests();
    }
}