using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Livit.Service;
using Livit.Model.ServiceObjects;
using Livit.Web.ViewModel;

namespace Livit.Web.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/leaves")]
    public class LeaveManagementController : Controller
    {
        protected readonly ILeaveManagementService LeaveManagementService;
        protected readonly IServiceObjectFactory ObjectFactory;

        public LeaveManagementController(ILeaveManagementService leaveService, IServiceObjectFactory objectFactory)
        {
            this.LeaveManagementService = leaveService;
            this.ObjectFactory = objectFactory;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> RequestLeave([FromBody] LeaveRequestViewModel leave)
        {
            if (leave == null)
                return BadRequest("Invalid request params");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var request = this.ObjectFactory.Create<LeaveServiceObject>(leave);

            var result = await this.LeaveManagementService.AddLeaveRequest(request);

            return Ok(result);
        }
    }
}
