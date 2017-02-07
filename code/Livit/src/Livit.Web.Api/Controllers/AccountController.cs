using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Livit.Service;
using Livit.Web.ViewModel;
using System.Net;

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

            return Ok(new LoginViewModel { Uri = url });
        }

        [Route("oauth2/callback")]
        [HttpGet]
        public async Task<IActionResult> GoogleCallback(string code)
        {
            return await Task.FromResult(Ok(this.UserService.Authorize(code)));
        }
    }
}
