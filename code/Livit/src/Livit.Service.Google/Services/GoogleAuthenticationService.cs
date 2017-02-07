namespace Livit.Service.Google.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Livit.Data.Repositories;
    using Livit.Infrastructure.Configurations;
    using System.Net;
    using Exceptions;
    using System.Net.Http;
    using global::Google.Apis.Auth.OAuth2.Responses;
    using Newtonsoft.Json;

    public class GoogleAuthenticationService : AuthenticationServiceBase, IAuthenticationService
    {
        protected readonly HttpClient Client;

        public GoogleAuthenticationService(IAsyncUnitOfWork unitOfWork,
            IAsyncDataLoader dataLoader, ILivitConfiguration configuration,
            HttpClient client)
            : base(unitOfWork, dataLoader, configuration)
        {
            this.Client = client;
        }

        public async override Task<string> GetGrantToManageLeaveRequests()
        {
            string url = null;

            var parameters = new Dictionary<string, string>
            {
                { "response_type", "code" },
                { "client_id", Configuration.Secrets.ClientId },
                { "scope", "https://www.googleapis.com/auth/calendar https://www.googleapis.com/auth/userinfo.email" },
                { "redirect_uri", Configuration.Secrets.RedirectUri },
                { "login_hint", "email" }
            };

            var query = string.Join("&", parameters.Select(m => m.Key + "=" + WebUtility.UrlEncode(m.Value)));

            url = Configuration.Secrets.AuthenUri + "?" + query;

            return await Task.FromResult(url);
        }

        public async override Task<bool> Authorize(string authorizationCode)
        {
            if (string.IsNullOrWhiteSpace(authorizationCode))
                throw new RequestArgumentNullException("The authorized_code is NULL.");

            var dic = new Dictionary<string, string>
            {
                { "code", authorizationCode },
                { "client_id", Configuration.Secrets.ClientId },
                { "client_secret", Configuration.Secrets.ClientSecret },
                { "grant_type", "authorization_code" },
                { "redirect_uri", Configuration.Secrets.RedirectUri }
            };

            var formContent = new FormUrlEncodedContent(dic);

            var response = await this.Client.PostAsync(Configuration.Secrets.TokenUri, formContent);
            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode)
            {
                return await Task.FromResult(false);
            }

            var result = await response.Content.ReadAsStringAsync();

            var token = JsonConvert.DeserializeObject<TokenResponse>(result);

            return await Task.FromResult(true);
        }
    }
}
