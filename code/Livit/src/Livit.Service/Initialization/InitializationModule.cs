namespace Livit.Service.Initialization
{
    using Livit.Infrastructure.Attributes;
    using Livit.Infrastructure.Initialization;
    using System;
    using System.Collections.Generic;
    using System.Text;

    [InitializableModule]
    public class InitializationModule : IInitializableModule
    {
        public void Initialize(InitializationContext context)
        {
            context.Services.AddScoped<ILeaveManagementService, LeaveManagementService>();
        }

        public void UnInitialize(InitializationContext context)
        {
        }
    }
}
