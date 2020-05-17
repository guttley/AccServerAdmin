using System;

namespace AccServerAdmin.Infrastructure.Helpers
{
    public static class TimeSpanExtensions
    {
        public static string ToGapFormat(this TimeSpan ts)
        {
            if (ts.TotalHours >= 1)
            {
                return ts.ToString(@"hh\:mm\:ss\.FFF");
            } 
            
            if (ts.TotalMinutes >= 1)
            {
                return ts.ToString(@"mm\:ss\.FFF");
            } 

            return ts.ToString(@"ss\.FFF");
        }
    }
}
