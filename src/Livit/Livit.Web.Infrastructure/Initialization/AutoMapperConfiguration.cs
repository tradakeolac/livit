namespace Livit.Web.Infrastructure.Initialization
{
    using AutoMapper;
    using Livit.Web.Infrastructure.AutoMapperProfiles;

    class AutoMapperConfiguration
    {
        internal static void Config()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<LeaveProfile>();
            });
        }
    }
}
