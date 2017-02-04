namespace Livit.Service
{
    using System;
    using System.Threading.Tasks;
    using Livit.Data.Repositories;
    using Model.ServiceObjects;

    public abstract class LeaveManagementService : LivitServiceBase, ILeaveManagementService
    {
        public LeaveManagementService(IAsyncUnitOfWork unitOfWork, IAsyncDataLoader dataLoader)
            : base(unitOfWork, dataLoader)
        {
        }

        public abstract Task<bool> AddLeaveRequest(LeaveServiceObject leaveObject);
    }
}
