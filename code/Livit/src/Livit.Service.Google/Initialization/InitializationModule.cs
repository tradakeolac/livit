namespace Livit.Service.Google.Initialization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Infrastructure.Initialization;

    [Livit.Infrastructure.Attributes.InitializableModule]
    public class InitializationModule : Livit.Infrastructure.Initialization.IInitializableModule
    {
        public void Initialize(InitializationContext context)
        {
            context.Services.AddScoped<ILeaveManagementService, Google.Calendar.GoogleCalendarServiceAdapter>();
        }

        public void UnInitialize(InitializationContext context)
        {
        }
    }
}
