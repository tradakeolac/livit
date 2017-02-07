using System.Threading.Tasks;
namespace Livit.Service
{

    public interface IAuthenticationService : IService
    {
        Task<string> GetGrantToManageLeaveRequests();
        Task<bool> Authorize(string authorizationCode);
    }
}
