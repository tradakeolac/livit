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

    public class GoogleCalendarServiceFactory : IGoogleCalendarServiceFactory
    {
        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "Google Calendar API .NET Quickstart";

        protected readonly IDataStore DataStore;
        protected readonly IGoogleObjectFactory ObjectFactory;
        protected readonly HttpClient HttpClient;

        public GoogleCalendarServiceFactory(IDataStore dataStore,
            IGoogleObjectFactory objectFactory, HttpClient httpClient)
        {
            this.DataStore = dataStore;
            this.ObjectFactory = objectFactory;
            this.HttpClient = httpClient;
        }

        public async Task<CalendarService> GetService(string email)
        {
            UserCredential credential = null;

            var token = await this.DataStore.GetAsync<TokenResponseEntity>(email);

            if (token == null)
            {
                token = ObjectFactory.Create<TokenResponseEntity>(await GetValue());
            }

            var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = "497145328019-7dl84cpo4d6n09ce2kdh34v46kvvmfm9.apps.googleusercontent.com",
                    ClientSecret = "tSpThzSrFZRecmP2agrewfwk"
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

        private async Task<TokenResponse> GetValue()
        {
            string code = "4/WeQUyU-l8SNmL5FslDJQ5pP_NCTzCB9NFJ6QIbw0NM4";

            var dic = new Dictionary<string, string>
            {
                { "code", code },
                { "client_id", "497145328019-7dl84cpo4d6n09ce2kdh34v46kvvmfm9.apps.googleusercontent.com" },
                { "client_secret", "tSpThzSrFZRecmP2agrewfwk" },
                { "grant_type", "authorization_code" },
                { "redirect_uri", "urn:ietf:wg:oauth:2.0:oob" }
            };

            var formContent = new FormUrlEncodedContent(dic);

            var response = await this.HttpClient.PostAsync("https://accounts.google.com/o/oauth2/token", formContent);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var token = JsonConvert.DeserializeObject<TokenResponse>(result);

            return await Task.FromResult(token);
        }
    }
}
