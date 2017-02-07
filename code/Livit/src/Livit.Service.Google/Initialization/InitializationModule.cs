﻿namespace Livit.Service.Google.Initialization
{
    using Infrastructure.Initialization;
    using System.Net.Http;
    using global::Google.Apis.Util.Store;
    using Services;

    [Livit.Infrastructure.Attributes.InitializableModule]
    public class InitializationModule : Livit.Infrastructure.Initialization.IInitializableModule
    {
        public void Initialize(InitializationContext context)
        {
            context.Services.AddScoped<IDataStore, DataStorage.DatabaseDataStore>();
            context.Services.AddScoped<ILeaveManagementService, GoogleCalendarServiceAdapter>();
            context.Services.AddSingleton<HttpClient, HttpClient>();
            context.Services.AddSingleton<IGoogleCalendarServiceFactory, GoogleCalendarServiceFactory>();
            context.Services.AddScoped<IAuthenticationService, GoogleAuthenticationService>();
            context.Services.AddScoped<ITokenService, TokenService>();
            context.Services.AddSingleton<IGoogleAuthorizationCodeFlowFactory, GoogleAuthorizationCodeFlowFactory>();
        }

        public void UnInitialize(InitializationContext context)
        {
        }
    }
}
