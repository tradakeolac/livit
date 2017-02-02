namespace Livit.Web.Infrastructure.Factories
{
    using Livit.Model.Entities;
    using Livit.Model.ServiceObjects;
    using Livit.Web.ViewModel;

    public class AutoMapperObjectFactory : IServiceObjectFactory, IEntityFactory, IViewModelFactory
    {
        public TDestination Create<TDestination>(object source) where TDestination : class, new()
        {
            return AutoMapper.Mapper.Map<TDestination>(source);
        }
    }
}
