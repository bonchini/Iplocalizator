using System;
using System.Collections.Generic;
using System.Linq;

namespace MP.IPLocalizator.Business.Calculators
{
    public static class TimeCalculator
    {
        public static string CalculateTime(List<string> timeZones)
        {
            List<string> result = new List<string>();
            if (timeZones != null)
            {
                var actualDate = DateTime.UtcNow;

                foreach (var timeZone in timeZones)
                {
                    var timeZoneInfo = TimeZoneInfo.GetSystemTimeZones().FirstOrDefault(x => x.DisplayName.Contains(timeZone));

                    if (timeZoneInfo != null)
                    {
                        var date = TimeZoneInfo.ConvertTimeFromUtc(actualDate, timeZoneInfo);

                        var dateString = $"{date.ToString("HH:mm:ss")} ({timeZone})";

                        result.Add(dateString);
                    }
                }
            }

            return string.Join(" o ", result);
            
        }
    }
}
