namespace Livit.Service
{
    using Livit.Model.ServiceObjects;
    using System.Threading.Tasks;

    public interface ILeaveManagementService : IService
    {
        Task<LeaveServiceObject> RequestLeaveAsync(LeaveServiceObject leaveObject);

        Task<bool> ApproveAsync(string eventId);

        Task<bool> RejectAsync(string eventId);
    }
}