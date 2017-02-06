namespace Livit.Service
{
    using System.Threading.Tasks;
    using Livit.Data.Repositories;
    using Model.ServiceObjects;

    public abstract class LeaveManagementService : LivitServiceBase, ILeaveManagementService
    {
        protected LeaveManagementService(IAsyncUnitOfWork unitOfWork, IAsyncDataLoader dataLoader)
            : base(unitOfWork, dataLoader)
        {
        }

        public abstract Task<bool> ApproveAsync(string eventId);
        public abstract Task<bool> RejectAsync(string eventId);
        public abstract Task<bool> RequestLeaveAsync(LeaveServiceObject leaveObject);
    }
}
