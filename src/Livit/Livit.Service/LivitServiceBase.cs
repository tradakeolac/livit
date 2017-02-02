namespace Livit.Service
{
    using Livit.Data.Repositories.Abstractions;
    using Livit.Data.Specifications;
    using Livit.Model;

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
