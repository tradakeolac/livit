namespace Livit.Infrastructure.Initialization
{
    using Configurations;
    using Livit.Infrastructure.Attributes;
    using Livit.Infrastructure.Ultility;

    [InitializableModule]
    public class InitializationModule : IInitializableModule
    {
        public void Initialize(InitializationContext context)
        {
            // Register service
            context.Services.AddSingleton<IDateTimeAdapter, DateTimeAdapter>();
            context.Services.AddSingleton<ILivitConfiguration, LivitConfiguration>();
        }

        public void UnInitialize(InitializationContext context)
        {
        }
    }
}