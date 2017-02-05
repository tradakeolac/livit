namespace Livit.Model.ServiceObjects
{
    public interface IServiceObjectFactory
    {
        TServiceObject Create<TServiceObject>(object source);
    }
}
