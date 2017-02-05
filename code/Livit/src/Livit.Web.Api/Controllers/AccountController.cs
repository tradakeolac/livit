using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Livit.Service;
using Livit.Web.ViewModel;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Livit.Web.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/account")]
    public class AccountController : Controller
    {
        protected readonly IUserService UserService;

        public AccountController(IUserService userService)
        {
            this.UserService = userService;
        }

        [Route("grantedCalendarUrl")]
        [HttpGet]
        public async Task<IActionResult> GetLoginUrl()
        {
            var url = await UserService.GetGrantToManageLeaveRequests();

            return Ok(new CalendarManagementGrantedUrlViewModel { Url = url });
        }
    }
}
