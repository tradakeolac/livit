namespace Livit.Service.Google.Initialization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Infrastructure.Initialization;
    using System.Net.Http;
    using global::Google.Apis.Util.Store;
    using Calendar;
    using Services;

    [Livit.Infrastructure.Attributes.InitializableModule]
    public class InitializationModule : Livit.Infrastructure.Initialization.IInitializableModule
    {
        public void Initialize(InitializationContext context)
        {
            context.Services.AddScoped<IDataStore, DataStorage.DatabaseDataStore>();
            context.Services.AddScoped<ILeaveManagementService, Google.Calendar.GoogleCalendarServiceAdapter>();
            context.Services.AddSingleton<HttpClient, HttpClient>();
            context.Services.AddScoped<IGoogleCalendarServiceFactory, GoogleCalendarServiceFactory>();
            context.Services.AddScoped<IAuthenticationService, GoogleAuthenticationService>();
        }

        public void UnInitialize(InitializationContext context)
        {
        }
    }
}
