namespace Livit.Service.Google.Calendar
{
    using global::Google.Apis.Calendar.v3.Data;
    using global::Google.Apis.Auth.OAuth2.Requests;
    using Livit.Service.Google.Models;
    using System;
    using System.Threading.Tasks;
    using Data.Repositories;
    using Model.ServiceObjects;
    using global::Google.Apis.Util.Store;
    using Exceptions;
    using Infrastructure.Configurations;
    using Infrastructure.Ultility;
    using System.Linq;

    public class GoogleCalendarServiceAdapter : LeaveManagementService, ILeaveManagementService
    {
        protected readonly IGoogleObjectFactory ObjectFactory;
        protected readonly IRepository Repository;
        protected readonly IDataStore DataStore;
        protected readonly IGoogleCalendarServiceFactory ServiceFactory;
        protected readonly ILivitConfiguration LivitConfiguration;
        protected readonly IDateTimeAdapter DateTimeAdapter;

        public GoogleCalendarServiceAdapter(IAsyncUnitOfWork unitOfWork,
            IAsyncDataLoader dataLoader, IRepository repository,
            IGoogleCalendarServiceFactory serviceFactory, IDateTimeAdapter dateTimeAdapter,
            IGoogleObjectFactory objectFactory, ILivitConfiguration configuration)
            : base(unitOfWork, dataLoader)
        {
            this.Repository = repository;
            this.ServiceFactory = serviceFactory;
            this.ObjectFactory = objectFactory;
            this.DateTimeAdapter = dateTimeAdapter;
            this.LivitConfiguration = configuration;
        }

        public override async Task<bool> RequestLeaveAsync(LeaveServiceObject leaveObject)
        {
            var newEvent = this.ObjectFactory.Create<Event>(leaveObject);

            // Update default value in setting file
            this.UpdateDefaultEvent(newEvent);

            string calendarId = "primary";
            var service = await this.ServiceFactory.GetService(leaveObject.EmployeeEmail, leaveObject.AuthorizeCode);

            Event createdEvent = null;

            try
            {
                var request = service.Events.Insert(newEvent, calendarId);

                createdEvent = await request.ExecuteAsync();
            }
            catch (Exception ex)
            {
                throw new AddActionException("Can not add the event to the Google calendar!");
            }

            return await Task.FromResult<bool>(createdEvent != null);
        }

        public override async Task<bool> ApproveAsync(string eventId)
        {
            await this.UpdateAsync(eventId, "accepted");

            return await Task.FromResult(true);
        }

        public override async Task<bool> RejectAsync(string eventId)
        {
            await this.UpdateAsync(eventId, "declined");

            return await Task.FromResult(true);
        }

        private async Task UpdateAsync(string requestedLeaveId, string status)
        {
            // Retrieve the event from the API
            var service = await this.ServiceFactory.GetService(this.LivitConfiguration.AdminEmail, "");

            var updateEvent = service.Events.Get("primary", requestedLeaveId).Execute();

            var admin = updateEvent.Attendees.FirstOrDefault(a => a.Email == this.LivitConfiguration.AdminEmail);
            admin.ResponseStatus = status;

            await service.Events.Update(updateEvent, "primary", updateEvent.Id).ExecuteAsync();
        }

        private void UpdateDefaultEvent(Event googleEvent)
        {
            // Add admin to attendee to notification & approvement
            googleEvent.Attendees.Add(new EventAttendee
            {
                Email = LivitConfiguration.AdminEmail
            });

            googleEvent.Start.TimeZone = this.DateTimeAdapter.DefaultTimeZone;
            googleEvent.End.TimeZone = this.DateTimeAdapter.DefaultTimeZone;
        }
    }
}
