using System.Threading.Tasks;
namespace Livit.Service
{

    public interface IAuthenticationService : IService
    {
        Task<string> GetGrantToManageLeaveRequests();
        Task Authorize(string authorizationCode);
    }
}
