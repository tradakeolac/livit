
namespace Livit.Web.Infrastructure.AutoMapperProfiles
{
    using Google.Apis.Auth.OAuth2.Responses;
    using Livit.Model.Entities;

    public class TokenProfile : AutoMapper.Profile
    {
        public TokenProfile()
        {
            CreateMap<TokenResponse, TokenResponseEntity>()
                .ForMember(m => m.Id, obj => obj.MapFrom(src => src.IdToken));
            CreateMap<TokenResponseEntity, TokenResponse>()
                .ForMember(m => m.IdToken, obj => obj.MapFrom(src => src.Id));
        }
    }
}
