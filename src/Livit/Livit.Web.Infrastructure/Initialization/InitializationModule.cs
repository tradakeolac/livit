namespace Livit.Web.Infrastructure.Initialization
{
    using System;
    using Livit.Infrastructure.Attributes;
    using Livit.Infrastructure.Initialization;
    using Livit.Model.Entities;
    using Livit.Web.Infrastructure.Factories;
    using Livit.Web.ViewModel;
    using Livit.Model.ServiceObjects;

    [InitializableModule]
    public class InitializationModule : Livit.Infrastructure.Initialization.IInitializableModule
    {
        public void Initialize(InitializationContext context)
        {
            context.Services.AddSingleton<IEntityFactory, AutoMapperObjectFactory>();
            context.Services.AddSingleton<IViewModelFactory, AutoMapperObjectFactory>();
            context.Services.AddSingleton<IServiceObjectFactory, AutoMapperObjectFactory>();
        }

        public void UnInitialize(InitializationContext context)
        {
        }
    }
}
