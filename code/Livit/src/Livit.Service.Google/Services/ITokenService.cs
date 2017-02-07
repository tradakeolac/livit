using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2.Responses;
using Livit.Service.Google.Models;

namespace Livit.Service.Google.Services
{
    public interface ITokenService
    {
        Task<TokenResponse> FetchToken(string authorizeCode);
        Task<VerifiedTokenResponse> VerifyToken(string idToken);
    }
}