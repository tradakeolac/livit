using Microsoft.Extensions.Configuration;

namespace Livit.Infrastructure.Configurations
{

    public class LivitConfiguration : ILivitConfiguration
    {
        protected readonly IConfiguration Configuration;

        public LivitConfiguration(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public LivitClientSecrets Secrets
        {
            get
            {
                return new LivitClientSecrets
                {
                    ClientId = Configuration.GetSection($"installed:{LivitKeys.ClientId}").Value,
                    ClientSecret = Configuration.GetSection($"installed:{LivitKeys.ClientSecret}").Value,
                    AuthenUri = Configuration.GetSection("installed:auth_uri").Value,
                    TokenUri = Configuration.GetSection("installed:token_uri").Value
                };
            }
        }
    }
}