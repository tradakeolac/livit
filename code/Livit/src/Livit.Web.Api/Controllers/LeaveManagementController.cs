using Livit.Model.ServiceObjects;
using Livit.Service;
using Livit.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Livit.Web.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/leaves")]
    public class LeaveManagementController : Controller
    {
        protected readonly ILeaveManagementService LeaveManagementService;
        protected readonly IServiceObjectFactory ObjectFactory;
        protected readonly IViewModelFactory ViewModelFactory;

        public LeaveManagementController(ILeaveManagementService leaveService,
            IServiceObjectFactory objectFactory, IViewModelFactory viewModelFactory)
        {
            this.LeaveManagementService = leaveService;
            this.ObjectFactory = objectFactory;
            this.ViewModelFactory = viewModelFactory;
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

            var result = await this.LeaveManagementService.RequestLeaveAsync(request);

            return Created("api/v1/leaves", this.ViewModelFactory.Create<LeaveResponseViewModel>(result));
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> Approve([FromRoute] string id, [FromBody] ChangeStatusLeaveViewModel payload)
        {
            var result = payload != null && payload.IsAccepted
                ? await this.LeaveManagementService.ApproveAsync(id)
                : await this.LeaveManagementService.RejectAsync(id);

            return Ok(new { success = result });
        }
    }
}