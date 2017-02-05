using System.Threading.Tasks;
namespace Livit.Service
{

    public interface IUserService : IService
    {
        Task<string> GetGrantToManageLeaveRequests();
    }
}
