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
    using Infrastructure.Configurations;
    using Infrastructure.Ultility;

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

        public override async Task<bool> AddLeaveRequest(LeaveServiceObject leaveObject)
        {
            var newEvent = this.ObjectFactory.Create<Event>(leaveObject);

            // Update default value in setting file
            this.UpdateDefaultEvent(newEvent);

            string calendarId = "primary";
            var service = await this.ServiceFactory.GetService(leaveObject.EmployeeEmail, leaveObject.AuthorizeCode);

            try
            {
                var request = service.Events.Insert(newEvent, calendarId);

                Event createdEvent = null;

                createdEvent = await request.ExecuteAsync();
            }
            catch (Exception ex)
            {
                throw new AddActionException("Can not add the event to the Google calendar!");
            }

            return await Task.FromResult<bool>(createdEvent != null);
        }

        private void UpdateDefaultEvent(Event googleEvent)
        {
            // Add admin to attendee to notification & approvement
            googleEvent.Attendees.Add(new EventAttendee()
            {
                Email = LivitConfiguration.AdminEmail
            });

            googleEvent.Start.TimeZone = this.DateTimeAdapter.DefaultTimeZone;
            googleEvent.End.TimeZone = this.DateTimeAdapter.DefaultTimeZone;
        }
    }
}
