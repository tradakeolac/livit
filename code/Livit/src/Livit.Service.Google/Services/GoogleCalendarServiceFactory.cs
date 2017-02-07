namespace Livit.Service.Google.Services
{
    using global::Google.Apis.Auth.OAuth2;
    using global::Google.Apis.Auth.OAuth2.Flows;
    using global::Google.Apis.Auth.OAuth2.Responses;
    using global::Google.Apis.Calendar.v3;
    using global::Google.Apis.Services;
    using global::Google.Apis.Util.Store;
    using Livit.Model.Entities;
    using Models;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Infrastructure.Configurations;

    public class GoogleCalendarServiceFactory : IGoogleCalendarServiceFactory
    {
        static readonly string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "Livit Leave Management Application";

        protected readonly IDataStore DataStore;
        protected readonly IGoogleObjectFactory ObjectFactory;
        protected readonly HttpClient HttpClient;
        protected readonly ILivitConfiguration Configuration;
        protected readonly IGoogleAuthorizationCodeFlowFactory AuthorizationFlowFactory;

        public GoogleCalendarServiceFactory(IDataStore dataStore,
            IGoogleObjectFactory objectFactory, HttpClient httpClient,
            ILivitConfiguration configuration,
            IGoogleAuthorizationCodeFlowFactory authorizationFlowFactory)
        {
            this.DataStore = dataStore;
            this.ObjectFactory = objectFactory;
            this.HttpClient = httpClient;
            this.Configuration = configuration;
            this.AuthorizationFlowFactory = authorizationFlowFactory;
        }

        public async Task<CalendarService> GetService(string email)
        {
            UserCredential credential = null;

            var token = await this.DataStore.GetAsync<TokenResponseEntity>(email);

            var flow = this.AuthorizationFlowFactory.CreateFlow();

            credential = new UserCredential(flow, email, ObjectFactory.Create<TokenResponse>(token));

            // Create Google Calendar API service.
            return await Task.FromResult(new CalendarService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName
            }));
        }
    }
}
