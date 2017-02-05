namespace Livit.Web.Infrastructure.Factories
{
    using AutoMapper;
    using Livit.Model.Entities;
    using Livit.Model.ServiceObjects;
    using Livit.Web.ViewModel;
    using Service.Google.Models;

    public class AutoMapperObjectFactory : IServiceObjectFactory, IEntityFactory, IViewModelFactory, IGoogleObjectFactory
    {
        private readonly IMapper mapper;

        public AutoMapperObjectFactory(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public TDestination Create<TDestination>(object source)
        {
            return mapper.Map<TDestination>(source);
        }
    }
}
