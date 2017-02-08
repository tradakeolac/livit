namespace Livit.Service.Google.Models
{
    public interface IGoogleObjectFactory
    {
        TGoogleEntity Create<TGoogleEntity>(object source);
    }
}