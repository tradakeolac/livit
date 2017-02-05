using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Livit.Service;
using Livit.Model.ServiceObjects;

namespace Livit.Web.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/leaves")]
    public class LeaveManagementController : Controller
    {
        private readonly ILeaveManagementService LeaveManagementService;

        public LeaveManagementController(ILeaveManagementService leaveService)
        {
            this.LeaveManagementService = leaveService;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddLeave()
        {
            var leave = new LeaveServiceObject
            {
                Description = "Cong xin nghi om!",
                EmployeeEmail = "nguyenthanhcongbkhn@gmail.com",
                From = DateTime.Now,
                To = DateTime.Now.AddDays(1),
                Summary = "Cong xin nghi om!"
            };

            var result = await this.LeaveManagementService.AddLeaveRequest(leave);

            return Ok(result);
        }
    }
}
