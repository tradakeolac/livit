namespace Livit.Infrastructure.Configurations
{
    public class LivitClientSecrets
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUri { get; set; }
        public string AuthenUri { get; set; }
        public string TokenUri { get; set; }
    }
}