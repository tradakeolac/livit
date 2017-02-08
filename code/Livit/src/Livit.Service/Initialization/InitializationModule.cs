namespace Livit.Service.Initialization
{
    using Livit.Infrastructure.Attributes;
    using Livit.Infrastructure.Initialization;

    [InitializableModule]
    public class InitializationModule : IInitializableModule
    {
        public void Initialize(InitializationContext context)
        {
        }

        public void UnInitialize(InitializationContext context)
        {
        }
    }
}