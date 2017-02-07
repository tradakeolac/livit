namespace Livit.Service.Google.Services
{
    using global::Google.Apis.Calendar.v3;
    using System.Threading.Tasks;

    public interface IGoogleCalendarServiceFactory
    {
        Task<CalendarService> GetService(string email);
    }
}