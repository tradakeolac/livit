namespace Livit.Service
{
    using Data.Repositories;
    using Infrastructure.Configurations;
    using System.Threading.Tasks;

    public abstract class AuthenticationServiceBase : LivitServiceBase, IAuthenticationService
    {
        protected readonly ILivitConfiguration Configuration;

        protected AuthenticationServiceBase(IAsyncUnitOfWork unitOfWork, IAsyncDataLoader dataLoader,
            ILivitConfiguration configuration)
            : base(unitOfWork, dataLoader)
        {
            this.Configuration = configuration;
        }

        public abstract Task Authorize(string authorizationCode);

        public abstract Task<string> GetGrantToManageLeaveRequests();
    }
}