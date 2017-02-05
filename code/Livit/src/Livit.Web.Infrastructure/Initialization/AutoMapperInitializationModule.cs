namespace Livit.Web.Infrastructure.Initialization
{
    using System;
    using AutoMapper;
    using Livit.Infrastructure.Attributes;
    using Livit.Infrastructure.Initialization;
    using Livit.Web.Infrastructure.AutoMapperProfiles;

    [InitializableModule]
    public class AutoMapperInitializationModule : Livit.Infrastructure.Initialization.IInitializableModule
    {
        public void Initialize(InitializationContext context)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<LeaveProfile>();
                cfg.AddProfile<TokenProfile>();
            });

            context.Services.AddSingleton<IMapper>(configuration.CreateMapper());
        }

        public void UnInitialize(InitializationContext context)
        {

        }
    }
}
