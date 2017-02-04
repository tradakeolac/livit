
namespace Livit.Infrastructure.Ultility
{
    using System;
    
    public interface IDateTimeAdapter
    {
        DateTime Now { get; }
        DateTime Utc { get; }
    }
}