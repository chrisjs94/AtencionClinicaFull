using System;

namespace ReportsAPI.Services
{
    public static class Helpers
    {
        internal static DateTime GetTimeInfo(DateTime date)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(date, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
        }
    }
}
