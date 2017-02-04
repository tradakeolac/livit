namespace Livit.Model.Entities
{
    public interface IEntityFactory
    {
        TEntity Create<TEntity>(object serviceObject) where TEntity : class, new();
    }
}
