using System;

namespace AccServerAdmin.Infrastructure.Helpers
{
    public static class TimeSpanExtensions
    {
        public static string ToGapFormat(this TimeSpan ts)
        {
            if (ts.TotalDays >= 1 | ts.TotalSeconds == 0)
            {
                return string.Empty;
            }

            var format = @"ss\.FFF";

            if (ts.TotalMinutes >= 1)
            {
                format = @"mm\:ss\.FFF";
            }

            if (ts.TotalHours >= 1)
            {
                format = @"hh\:mm\:ss\.FFF";
            } 
            
            return $"+{ts.ToString(format)}";
        }
    }
}
