namespace Livit.Web.Infrastructure.AutoMapperProfiles
{
    using Google.Apis.Calendar.v3.Data;
    using Livit.Model.Entities;
    using Livit.Model.ServiceObjects;
    using ViewModel;

    class LeaveProfile : AutoMapper.Profile
    {
        public LeaveProfile()
        {
            CreateMap<LeaveServiceObject, RequestedLeaveEntity>()
                .ForMember(m => m.Name, obj => obj.MapFrom(src => src.Summary))
                .ForMember(m => m.EmployeeId, obj => obj.Ignore())
                .ForMember(m => m.Employee, obj => obj.Ignore())
                .ForMember(m => m.ApprovedDate, obj => obj.Ignore());


            CreateMap<LeaveServiceObject, Event>()
                .ForMember(m => m.Summary, obj => obj.MapFrom(src => src.Summary))
                .ForMember(m => m.Description, obj => obj.MapFrom(src => src.Description))
                .ForMember(m => m.Start, obj => obj.MapFrom(src => new EventDateTime
                {
                    DateTime = src.From,
                    TimeZone = "America/Los_Angeles" // Hardcoded default time-zone
                }))
                .ForMember(m => m.End, obj => obj.MapFrom(src => new EventDateTime
                {
                    DateTime = src.To,
                    TimeZone = "America/Los_Angeles" // Hardcoded default time-zone
                }))
                .ForMember(m => m.Reminders, obj => obj.MapFrom(src => new Event.RemindersData
                {
                    UseDefault = false,
                    Overrides = new EventReminder[] {
                        new EventReminder { Method = "email", Minutes = 24 * 60 }
                    }
                }))
                .ForMember(m => m.Attendees, obj => obj.MapFrom(src => new EventAttendee[] {
                    new EventAttendee { Email = "mitom3bua@gmail.com" },
                    new EventAttendee { Email = src.EmployeeEmail }
                }))
                .ForAllOtherMembers(src => src.Ignore());


            CreateMap<LeaveRequestViewModel, LeaveServiceObject>()
                .ForMember(m => m.Id, obj => obj.Ignore());
        }
    }
}
