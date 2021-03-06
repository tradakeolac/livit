﻿namespace Livit.Service.Google.Services
{
    using Exceptions;
    using global::Google.Apis.Util.Store;
    using Livit.Data.Repositories;
    using Livit.Infrastructure.Configurations;
    using Model.ServiceObjects;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class GoogleAuthenticationService : AuthenticationServiceBase, IAuthenticationService
    {
        protected readonly HttpClient Client;
        protected readonly IDataStore DataStore;
        protected readonly ITokenService TokenService;
        protected readonly IServiceObjectFactory ObjectFactory;
        protected readonly IGoogleAuthorizationCodeFlowFactory AuthorizationFlowFactory;

        public GoogleAuthenticationService(IAsyncUnitOfWork unitOfWork,
            IAsyncDataLoader dataLoader, ILivitConfiguration configuration,
            HttpClient client, IDataStore dataStore, ITokenService tokenService,
            IGoogleAuthorizationCodeFlowFactory authorizationFlowFactory,
            IServiceObjectFactory objectFactory)
            : base(unitOfWork, dataLoader, configuration)
        {
            this.Client = client;
            this.DataStore = dataStore;
            this.TokenService = tokenService;
            this.ObjectFactory = objectFactory;
            this.AuthorizationFlowFactory = authorizationFlowFactory;
        }

        public async override Task<string> GetGrantToManageLeaveRequests()
        {
            var flow = this.AuthorizationFlowFactory.CreateFlow();

            var uri = flow.CreateAuthorizationCodeRequest(Configuration.Secrets.RedirectUri).Build();
            return await Task.FromResult(uri.AbsoluteUri);
        }

        public async override Task<ExternalUserServiceObject> Authorize(string authorizationCode)
        {
            Guard.EnsureStringNotNullOrEmpty(authorizationCode, nameof(authorizationCode));

            var token = await this.TokenService.FetchToken(authorizationCode);

            if (token == null)
                throw new System.NullReferenceException()
                    .ToBusinessException<CommonException>("Can not get the access_token.");

            var tokenInfo = await this.TokenService.VerifyToken(token.IdToken);

            await this.DataStore.StoreAsync(tokenInfo.Email, token);

            return this.ObjectFactory.Create<ExternalUserServiceObject>(tokenInfo);
        }
    }
}