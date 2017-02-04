namespace Livit.Infrastructure.Initialization
{
    public class InitializationContext
    {
        public InitializationContext(ILivitServiceCollection services)
        {
            this.Services = services;
        }

        public ILivitServiceCollection Services { get; }
    }
}
