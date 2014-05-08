using System;

namespace AtlassianStashSharp.Helpers
{
    public static class UnixDateTimeHelper
    {
        public static DateTime FromUnixTime(long t)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return start.AddMilliseconds(t).ToLocalTime();
        }
    }
}

