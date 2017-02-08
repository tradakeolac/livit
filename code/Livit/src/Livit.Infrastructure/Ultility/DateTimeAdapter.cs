namespace Livit.Infrastructure.Ultility
{
    using Configurations;
    using System;

    public class DateTimeAdapter : IDateTimeAdapter
    {
        private readonly ILivitConfiguration Configuration;

        public DateTimeAdapter(ILivitConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public string DefaultTimeZone
        {
            get { return this.Configuration.DefaultTimeZone; }
        }

        public DateTime Max
        {
            get
            {
                return DateTime.MaxValue;
            }
        }

        public DateTime Min
        {
            get
            {
                return DateTime.MinValue;
            }
        }

        public DateTime Now
        {
            get { return DateTime.Now; }
        }

        public DateTime Utc
        {
            get { return DateTime.UtcNow; }
        }
    }
}