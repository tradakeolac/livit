namespace Livit.Infrastructure.Initialization
{
    using Livit.Infrastructure.Attributes;
    using Livit.Infrastructure.Ultility;

    [InitializableModule]
    public class InitializationModule : IInitializableModule
    {
        public void Initialize(InitializationContext context)
        {
            // Register service
            context.Services.AddSingleton<IDateTimeAdapter, DateTimeAdapter>();
        }

        public void UnInitialize(InitializationContext context)
        {
        }
    }
}
