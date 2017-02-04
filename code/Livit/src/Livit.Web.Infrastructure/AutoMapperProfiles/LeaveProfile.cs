namespace Livit.Web.Infrastructure.AutoMapperProfiles
{
    using Livit.Model.Entities;
    using Livit.Model.ServiceObjects;

    class LeaveProfile : AutoMapper.Profile
    {
        public LeaveProfile()
        {
            CreateMap<LeaveServiceObject, RequestedLeaveEntity>()
                .ForMember(m => m.Name, obj => obj.MapFrom(src => src.Summary))
                .ForMember(m => m.EmployeeId, obj => obj.Ignore())
                .ForMember(m => m.Employee, obj => obj.Ignore())
                .ForMember(m => m.ApprovedDate, obj => obj.Ignore());
        }
    }
}
