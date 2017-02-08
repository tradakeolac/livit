namespace Livit.Infrastructure.Ultility
{
    using System;

    public interface IDateTimeAdapter
    {
        DateTime Now { get; }
        DateTime Min { get; }
        DateTime Max { get; }
        DateTime Utc { get; }
        string DefaultTimeZone { get; }
    }
}