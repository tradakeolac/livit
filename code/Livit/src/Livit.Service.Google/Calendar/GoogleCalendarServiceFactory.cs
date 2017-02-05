namespace Livit.Service.Google.Calendar
{
    using global::Google.Apis.Auth.OAuth2;
    using global::Google.Apis.Auth.OAuth2.Flows;
    using global::Google.Apis.Auth.OAuth2.Responses;
    using global::Google.Apis.Calendar.v3;
    using global::Google.Apis.Services;
    using global::Google.Apis.Util.Store;
    using Livit.Model.Entities;
    using Models;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Infrastructure.Configurations;
    using Exceptions;

    public class GoogleCalendarServiceFactory : IGoogleCalendarServiceFactory
    {
        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "Livit Leave Management Application";

        protected readonly IDataStore DataStore;
        protected readonly IGoogleObjectFactory ObjectFactory;
        protected readonly HttpClient HttpClient;
        protected readonly ILivitConfiguration Configuration;

        public GoogleCalendarServiceFactory(IDataStore dataStore,
            IGoogleObjectFactory objectFactory, HttpClient httpClient,
            ILivitConfiguration configuration)
        {
            this.DataStore = dataStore;
            this.ObjectFactory = objectFactory;
            this.HttpClient = httpClient;
            this.Configuration = configuration;
        }

        public async Task<CalendarService> GetService(string email, string authorizeCode)
        {
            UserCredential credential = null;

            var token = await this.DataStore.GetAsync<TokenResponseEntity>(email);

            if (token == null)
            {
                var responseToken = await FetchToken(authorizeCode);

                if (responseToken == null)
                    throw new CommonException("Can not fetch access_token from Google server.");

                token = ObjectFactory.Create<TokenResponseEntity>(responseToken);
            }

            var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = Configuration.Secrets.ClientId,
                    ClientSecret = Configuration.Secrets.ClientSecret
                },
                Scopes = Scopes,
                DataStore = this.DataStore
            });

            credential = new UserCredential(flow, email, ObjectFactory.Create<TokenResponse>(token));

            // Create Google Calendar API service.
            return await Task.FromResult(new CalendarService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName
            }));
        }

        private async Task<TokenResponse> FetchToken(string authorizeCode)
        {
            if (string.IsNullOrWhiteSpace(authorizeCode))
                throw new RequestArgumentNullException("The authorized_code is NULL.");

            var dic = new Dictionary<string, string>
            {
                { "code", authorizeCode },
                { "client_id", Configuration.Secrets.ClientId },
                { "client_secret", Configuration.Secrets.ClientSecret },
                { "grant_type", "authorization_code" },
                { "redirect_uri", "urn:ietf:wg:oauth:2.0:oob" }
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
    }
}
