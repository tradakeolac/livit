﻿namespace Livit.Web.Infrastructure.Initialization
{
    using Livit.Data.EntityFramework;
    using Livit.Data.Repositories;
    using Livit.Infrastructure.Attributes;
    using Livit.Infrastructure.Initialization;
    using Livit.Model.Entities;
    using Livit.Model.ServiceObjects;
    using Livit.Web.Infrastructure.Factories;
    using Livit.Web.ViewModel;
    using Service.Google.Models;

    [InitializableModule]
    public class InitializationModule : Livit.Infrastructure.Initialization.IInitializableModule
    {
        public void Initialize(InitializationContext context)
        {
            context.Services.AddSingleton<IEntityFactory, AutoMapperObjectFactory>();
            context.Services.AddSingleton<IViewModelFactory, AutoMapperObjectFactory>();
            context.Services.AddSingleton<IServiceObjectFactory, AutoMapperObjectFactory>();
            context.Services.AddSingleton<IGoogleObjectFactory, AutoMapperObjectFactory>();

            context.Services.AddScoped<IAsyncDataLoader, EFCoreDataLoader>();
            context.Services.AddScoped<IDataLoaderRepository, EFCoreDataLoader>();
            context.Services.AddScoped<IRepository, EFCoreRepository>();
            context.Services.AddScoped<IAsyncUnitOfWork, EFCoreUnitOfWork>();
            context.Services.AddScoped<IUnitOfWork, EFCoreUnitOfWork>();
        }

        public void UnInitialize(InitializationContext context)
        {
        }
    }
}