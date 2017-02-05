namespace Livit.Service.Initialization
{
    using Livit.Infrastructure.Attributes;
    using Livit.Infrastructure.Initialization;

    [InitializableModule]
    public class InitializationModule : IInitializableModule
    {
        public void Initialize(InitializationContext context)
        {
            context.Services.AddScoped<IUserService, UserService>();
        }

        public void UnInitialize(InitializationContext context)
        {
        }
    }
}
