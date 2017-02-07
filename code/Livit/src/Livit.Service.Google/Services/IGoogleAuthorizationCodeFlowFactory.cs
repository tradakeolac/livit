namespace Livit.Service.Google.Services
{
    using global::Google.Apis.Auth.OAuth2.Flows;

    public interface IGoogleAuthorizationCodeFlowFactory
    {
        IAuthorizationCodeFlow CreateFlow();
    }
}