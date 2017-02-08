using Livit.Model.ServiceObjects;
using Livit.Service.Google.Models;

namespace Livit.Web.Infrastructure.AutoMapperProfiles
{
    public class UserProfile : AutoMapper.Profile
    {
        public UserProfile()
        {
            this.CreateMap<VerifiedTokenResponse, ExternalUserServiceObject>()
                .ForMember(m => m.ProviderUserId, obj => obj.MapFrom(src => src.UserId));
        }
    }
}
