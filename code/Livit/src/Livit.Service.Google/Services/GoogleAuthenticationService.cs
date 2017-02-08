namespace Livit.Service.Google.Services
{
    using Exceptions;
    using global::Google.Apis.Util.Store;
    using Livit.Data.Repositories;
    using Livit.Infrastructure.Configurations;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class GoogleAuthenticationService : AuthenticationServiceBase, IAuthenticationService
    {
        protected readonly HttpClient Client;
        protected readonly IDataStore DataStore;
        protected readonly ITokenService TokenService;
        protected readonly IGoogleAuthorizationCodeFlowFactory AuthorizationFlowFactory;

        public GoogleAuthenticationService(IAsyncUnitOfWork unitOfWork,
            IAsyncDataLoader dataLoader, ILivitConfiguration configuration,
            HttpClient client, IDataStore dataStore, ITokenService tokenService,
            IGoogleAuthorizationCodeFlowFactory authorizationFlowFactory)
            : base(unitOfWork, dataLoader, configuration)
        {
            this.Client = client;
            this.DataStore = dataStore;
            this.TokenService = tokenService;
            this.AuthorizationFlowFactory = authorizationFlowFactory;
        }

        public async override Task<string> GetGrantToManageLeaveRequests()
        {
            var flow = this.AuthorizationFlowFactory.CreateFlow();

            var uri = flow.CreateAuthorizationCodeRequest(Configuration.Secrets.RedirectUri).Build();
            return await Task.FromResult(uri.AbsoluteUri);
        }

        public async override Task Authorize(string authorizationCode)
        {
            if (string.IsNullOrWhiteSpace(authorizationCode))
                throw new RequestArgumentNullException("The authorized_code is NULL.");

            var token = await this.TokenService.FetchToken(authorizationCode);

            if (token == null)
                throw new CommonException("Can not get the access_token.");

            var tokenInfo = await this.TokenService.VerifyToken(token.IdToken);

            await this.DataStore.StoreAsync(tokenInfo.Email, token);
        }
    }
}