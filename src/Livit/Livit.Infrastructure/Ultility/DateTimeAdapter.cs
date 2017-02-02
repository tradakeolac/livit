namespace Livit.Infrastructure.Ultility
{
    using System;

    public class DateTimeAdapter : IDateTimeAdapter
    {
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
