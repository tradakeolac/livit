namespace Livit.Service
{
    using Livit.Data.Repositories;

    public class LeaveManagementService : LivitServiceBase, ILeaveManagementService
    {
        public LeaveManagementService(IAsyncUnitOfWork unitOfWork, IAsyncDataLoader dataLoader) 
            : base(unitOfWork, dataLoader)
        {
        }
    }
}
