using System;

namespace MP.IPLocalizator.Business.Calculators
{
    public static class DistanceCalculator
    {
        public const double latitudeCABA = -34.687400817871094;
        public const double longitudeCABA = -58.56330108642578;
        
        public static double? CalculateDistance(double? latitude, double? longitude)
        {
            if(latitude.HasValue && longitude.HasValue)
            {
                double rlat1 = Math.PI * latitude.Value / 180;
                double rlat2 = Math.PI * latitudeCABA / 180;
                double theta = longitude.Value - longitudeCABA;
                double rtheta = Math.PI * theta / 180;
                double dist =
                    Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                    Math.Cos(rlat2) * Math.Cos(rtheta);
                dist = Math.Acos(dist);
                dist = dist * 180 / Math.PI;
                dist = dist * 60 * 1.1515 * 1.609344;
                return Math.Truncate(dist * 100) / 100;
            }
            else
            {
                return null;
            }
        }
    }
}
