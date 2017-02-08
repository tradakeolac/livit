using Livit.Model.ServiceObjects;
using System.Threading.Tasks;

namespace Livit.Service
{
    public interface IAuthenticationService : IService
    {
        Task<string> GetGrantToManageLeaveRequests();

        Task<ExternalUserServiceObject> Authorize(string authorizationCode);
    }
}