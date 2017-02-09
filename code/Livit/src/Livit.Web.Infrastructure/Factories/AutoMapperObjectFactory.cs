namespace Livit.Web.Infrastructure.Factories
{
    using AutoMapper;
    using Livit.Model.Entities;
    using Livit.Model.ServiceObjects;
    using Livit.Web.ViewModel;
    using Service.Exceptions;
    using Service.Google.Models;
    using System;

    public class AutoMapperObjectFactory : IServiceObjectFactory, IEntityFactory, IViewModelFactory, IGoogleObjectFactory
    {
        private readonly IMapper mapper;

        public AutoMapperObjectFactory(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public TDestination Create<TDestination>(object source)
        {
            Guard.EnsureRequestNotNull(source, nameof(source) + $" map to type {typeof(TDestination).Name}");

            try
            {
                return mapper.Map<TDestination>(source);
            }
            catch
            {
                throw new MappingException($"Can not mapping to type {typeof(TDestination).Name} from source type {source.GetType().Name}");
            }
        }
    }
}