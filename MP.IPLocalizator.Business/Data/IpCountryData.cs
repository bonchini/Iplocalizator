using System.Collections.Generic;

namespace MP.IPLocalizator.Business.Data
{
    public class IpCountryData
    {
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Languages { get; set; }
    }
}
