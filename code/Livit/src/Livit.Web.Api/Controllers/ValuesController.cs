using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Livit.Service;
using Livit.Model.ServiceObjects;

namespace Livit.Web.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ILeaveManagementService LeaveManagementService;

        public ValuesController(ILeaveManagementService leaveService)
        {
            this.LeaveManagementService = leaveService;
        }

        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var leave = new LeaveServiceObject()
            {
                Description = "Cong",
                EmployeeEmail = "nguyenthanhcongbkhn@gmail.com",
                From = DateTime.Now,
                To = DateTime.Now.AddDays(1),
                Summary = "Nghi om"
            };

            var s = await this.LeaveManagementService.AddLeaveRequest(leave);

            var re = new string[] { "value1", "value2" };

            return await Task.FromResult(re);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
