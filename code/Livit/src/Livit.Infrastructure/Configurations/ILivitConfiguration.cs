namespace Livit.Infrastructure.Configurations
{
    public interface ILivitConfiguration
    {
        T Get<T>();
        T Get<T>(string key);
    }
}
