namespace Livit.Service
{
    using Livit.Data.Repositories;

    public abstract class LivitServiceBase
    {
        protected readonly IAsyncDataLoader DataLoader;
        protected readonly IAsyncUnitOfWork UnitOfWork;

        protected LivitServiceBase(IAsyncUnitOfWork unitOfWork, IAsyncDataLoader dataLoader)
        {
            this.UnitOfWork = unitOfWork;
            this.DataLoader = dataLoader;
        }
    }
}