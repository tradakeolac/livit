namespace Livit.Service
{
    using Livit.Model.ServiceObjects;
    using System.Threading.Tasks;

    public interface ILeaveManagementService : IService
    {
        Task<bool> AddLeaveRequest(LeaveServiceObject leaveObject);
    }
}
