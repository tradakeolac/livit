namespace Livit.Infrastructure.Initialization
{
    public interface IInitializableModule
    {
        void Initialize(InitializationContext context);

        void UnInitialize(InitializationContext context);
    }
}