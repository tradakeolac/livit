namespace Livit.Web.Infrastructure.Initialization
{
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
                cfg.AddProfile<UserProfile>();
            });

            context.Services.AddSingleton<IMapper>(configuration.CreateMapper());
        }

        public void UnInitialize(InitializationContext context)
        {
        }
    }
}