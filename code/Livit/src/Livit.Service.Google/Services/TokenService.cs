namespace Livit.Service.Google.Services
{
    using global::Google.Apis.Auth.OAuth2.Responses;
    using Livit.Infrastructure.Configurations;
    using Livit.Service.Exceptions;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Models;

    public class TokenService : ITokenService
    {
        protected readonly ILivitConfiguration Configuration;
        protected readonly HttpClient HttpClient;

        private const string GoogleApiTokenInfoUrl = "https://www.googleapis.com/oauth2/v3/tokeninfo?id_token={0}";

        public TokenService(ILivitConfiguration configuration, HttpClient client)
        {
            this.Configuration = configuration;
            this.HttpClient = client;
        }

        public async Task<TokenResponse> FetchToken(string authorizeCode)
        {
            if (string.IsNullOrWhiteSpace(authorizeCode))
                throw new RequestArgumentNullException("The authorized_code is NULL.");

            var dic = new Dictionary<string, string>
            {
                { "code", authorizeCode },
                { "client_id", Configuration.Secrets.ClientId },
                { "client_secret", Configuration.Secrets.ClientSecret },
                { "grant_type", "authorization_code" },
                { "redirect_uri", Configuration.Secrets.RedirectUri }
            };

            var formContent = new FormUrlEncodedContent(dic);

            var response = await this.HttpClient.PostAsync(Configuration.Secrets.TokenUri, formContent);
            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = await response.Content.ReadAsStringAsync();

            var token = JsonConvert.DeserializeObject<TokenResponse>(result);

            return await Task.FromResult(token);
        }

        public async Task<VerifiedTokenResponse> VerifyToken(string idToken)
        {
            if (string.IsNullOrEmpty(idToken))
                throw new RequestArgumentNullException("Id token can not be null.");

            var response = await this.HttpClient.GetAsync(string.Format(GoogleApiTokenInfoUrl, idToken));
            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = await response.Content.ReadAsStringAsync();

            var token = JsonConvert.DeserializeObject<VerifiedTokenResponse>(result);

            return token;
        }
    }
}
