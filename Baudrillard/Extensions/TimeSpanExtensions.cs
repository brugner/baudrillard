using System;

namespace Baudrillard.Extensions
{
    public static class TimeSpanExtensions
    {
        public static TimeSpan? StripMilliseconds(this TimeSpan? time)
        {
            if (time.HasValue)
            {
                return new TimeSpan(time.Value.Days, time.Value.Hours, time.Value.Minutes, time.Value.Seconds);
            }
            else
            {
                return null;
            }
        }

        public static TimeSpan StripMilliseconds(this TimeSpan time)
        {
            return new TimeSpan(time.Days, time.Hours, time.Minutes, time.Seconds);
        }
    }
}
