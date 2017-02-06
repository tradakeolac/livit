using System;
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

        public string AdminEmail
        {
            get { return Configuration.GetValue<string>("AdminEmail"); }
        }

        public string DefaultTimeZone
        {
            get { return Configuration.GetValue<string>("DefaultTimeZone"); }
        }

        public LivitClientSecrets Secrets
        {
            get
            {
                return new LivitClientSecrets
                {
                    ClientId = Configuration.GetValue<string>($"web:{LivitKeys.ClientId}"),
                    ClientSecret = Configuration.GetValue<string>($"web:{LivitKeys.ClientSecret}"),
                    AuthenUri = Configuration.GetValue<string>($"web:{LivitKeys.AuthenUri}"),
                    TokenUri = Configuration.GetValue<string>($"web:{LivitKeys.TokenUri}"),
                    RedirectUri = Configuration.GetValue<string>($"web:{LivitKeys.RedirectUri}")
                };
            }
        }
    }
}