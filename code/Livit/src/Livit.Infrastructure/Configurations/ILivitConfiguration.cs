namespace Livit.Infrastructure.Configurations
{
    public interface ILivitConfiguration
    {
        LivitClientSecrets Secrets { get; }
        string AdminEmail { get; }
        string DefaultTimeZone { get; }
        string[] CalendarScopes { get; }
    }
}