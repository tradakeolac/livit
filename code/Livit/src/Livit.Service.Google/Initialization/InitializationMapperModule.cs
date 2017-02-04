namespace Livit.Service.Google.Initialization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Infrastructure.Initialization;
    using AutoMapper;
    using Model.ServiceObjects;
    using global::Google.Apis.Calendar.v3.Data;

    [Livit.Infrastructure.Attributes.InitializableModule]
    public class InitializationMapperModule : Livit.Infrastructure.Initialization.IInitializableModule
    {
        public void Initialize(InitializationContext context)
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<LeaveServiceObject, Event>()
                .ForMember(m => m.Start, obj => obj.MapFrom(src => new EventDateTime
                {
                    DateTime = src.From,
                    TimeZone = "America/Los_Angeles"
                }))
                .ForMember(m => m.End, obj => obj.MapFrom(src => new EventDateTime
                {
                    DateTime = src.To,
                    TimeZone = "America/Los_Angeles"
                }))
                .ForMember(m => m.Reminders, obj => obj.MapFrom(src => new Event.RemindersData
                {
                    UseDefault = false,
                    Overrides = new EventReminder[] {
                        new EventReminder { Method = "email", Minutes = 24 * 60 }
                    }
                }))
                .ForMember(m => m.Attendees, obj => obj.MapFrom(src => new EventAttendee[] {
                    new EventAttendee { Email = "admin@example.com" },
                    new EventAttendee { Email = src.EmployeeEmail }
                }))
                .ForAllOtherMembers(src => src.Ignore());
            });
        }

        public void UnInitialize(InitializationContext context)
        {
        }
    }
}
