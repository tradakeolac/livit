namespace Livit.Infrastructure.Configurations
{
    public interface ILivitConfiguration
    {
        LivitClientSecrets Secrets { get; }
    }
}
