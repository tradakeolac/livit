namespace Livit.Service.Google.Calendar
{
    using global::Google.Apis.Calendar.v3.Data;
    using Livit.Service.Google.Models;
    using System;
    using System.Threading.Tasks;
    using Data.Repositories;
    using Model.ServiceObjects;
    using global::Google.Apis.Util.Store;
    using Exceptions;

    public class GoogleCalendarServiceAdapter : LeaveManagementService, ILeaveManagementService
    {
        protected readonly IGoogleObjectFactory ObjectFactory;
        protected readonly IRepository Repository;
        protected readonly IDataStore DataStore;
        protected readonly IGoogleCalendarServiceFactory ServiceFactory;

        public GoogleCalendarServiceAdapter(IAsyncUnitOfWork unitOfWork,
            IAsyncDataLoader dataLoader, IRepository repository,
            IGoogleCalendarServiceFactory serviceFactory,
            IGoogleObjectFactory objectFactory)
            : base(unitOfWork, dataLoader)
        {
            this.Repository = repository;
            this.ServiceFactory = serviceFactory;
            this.ObjectFactory = objectFactory;
        }

        public override async Task<bool> AddLeaveRequest(LeaveServiceObject leaveObject)
        {
            var newEvent = this.ObjectFactory.Create<Event>(leaveObject);

            string calendarId = "primary";
            var service = await this.ServiceFactory.GetService(leaveObject.EmployeeEmail, leaveObject.AuthorizeCode);

            var request = service.Events.Insert(newEvent, calendarId);

            Event createdEvent = null;
            try
            {
                createdEvent = await request.ExecuteAsync();
            }
            catch (Exception ex)
            {
                throw new AddActionException("Can not add the event to the Google calendar!");
            }

            return await Task.FromResult<bool>(createdEvent != null);
        }
    }
}
