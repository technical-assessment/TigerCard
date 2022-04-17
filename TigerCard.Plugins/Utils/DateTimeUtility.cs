using System;
namespace TigerCard.Plugins
{
    public static class DateTimeUtility
    {
        public static DateTime FirstDateInWeek(this DateTime dt, DayOfWeek weekStartDay)
        {
            while (dt.DayOfWeek != weekStartDay)
                dt = dt.AddDays(-1);
            return dt;
        }
    }
}
