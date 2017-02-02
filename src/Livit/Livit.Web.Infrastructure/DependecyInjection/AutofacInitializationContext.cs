namespace Livit.Web.Infrastructure.DependencyInjection
{
    using Livit.Infrastructure.Initialization;

    public class AutofacInitializationContext : InitializationContext
    {
        public AutofacInitializationContext(ILivitServiceCollection services) : base(services)
        {

        }
    }
}
