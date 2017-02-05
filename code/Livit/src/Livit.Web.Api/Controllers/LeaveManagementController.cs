using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Livit.Service;
using Livit.Model.ServiceObjects;

namespace Livit.Web.Api.Controllers
{
    [Route("api/leaves")]
    public class LeaveManagementController : Controller
    {
        private readonly ILeaveManagementService LeaveManagementService;

        public LeaveManagementController(ILeaveManagementService leaveService)
        {
            this.LeaveManagementService = leaveService;
        }

        [HttpPost]
        [Route("")]
        public async Task<bool> AddLeave()
        {
            var leave = new LeaveServiceObject
            {
                Description = "Cong xin nghi om!",
                EmployeeEmail = "nguyenthanhcongbkhn@gmail.com",
                From = DateTime.Now,
                To = DateTime.Now.AddDays(1),
                Summary = "Nghi om"
            };

            return await this.LeaveManagementService.AddLeaveRequest(leave);
        }
    }
}
