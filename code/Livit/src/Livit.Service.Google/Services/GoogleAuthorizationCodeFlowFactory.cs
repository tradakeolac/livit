namespace Livit.Service.Google.Services
{
    using global::Google.Apis.Auth.OAuth2;
    using global::Google.Apis.Auth.OAuth2.Flows;
    using global::Google.Apis.Util.Store;
    using Infrastructure.Configurations;

    public class GoogleAuthorizationCodeFlowFactory : IGoogleAuthorizationCodeFlowFactory
    {
        protected readonly ILivitConfiguration Configuration;
        protected readonly IDataStore DataStore;

        public GoogleAuthorizationCodeFlowFactory(ILivitConfiguration configuration, IDataStore dataStore)
        {
            this.Configuration = configuration;
            this.DataStore = dataStore;
        }

        public IAuthorizationCodeFlow CreateFlow()
        {
            return new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = Configuration.Secrets.ClientId,
                    ClientSecret = Configuration.Secrets.ClientSecret
                },
                Scopes = Configuration.CalendarScopes,
                DataStore = this.DataStore
            });
        }
    }
}
