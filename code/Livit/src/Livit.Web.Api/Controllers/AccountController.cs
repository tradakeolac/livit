using Livit.Service;
using Livit.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Livit.Web.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    public class AccountController : Controller
    {
        protected readonly IAuthenticationService UserService;

        public AccountController(IAuthenticationService userService)
        {
            this.UserService = userService;
        }

        [Route("oauth2/authorize")]
        [HttpGet]
        public async Task<IActionResult> GetLoginUrl()
        {
            var url = await UserService.GetGrantToManageLeaveRequests();

            throw new ArgumentNullException("halla");

            return Ok(new LoginViewModel { Uri = url });
        }

        [Route("oauth2/callback")]
        [HttpGet]
        public async Task<IActionResult> GoogleCallback([FromQuery] string code)
        {
            var identity = User.Identity as ClaimsIdentity;
            var externalUserInfo = await this.UserService.Authorize(code);

            if (externalUserInfo != null && identity != null)
            {
                identity.AddClaim(new Claim(ClaimTypes.Email, externalUserInfo.Email));
            }

            return await Task.FromResult(Ok(new { success = true }));
        }
    }
}