using System;

namespace MP.IPLocalizator.Business.Data
{
    public class IpDataResponse
    {
        public string Ip { get; set; }
        public DateTime ActualLocalDate { get; set; }
        public string Country { get; set; }
        public string IsoCode { get; set; }
        public string Languages { get; set; }
        public string Currency { get; set; }
        public string CountryHours { get; set; }
        public string EstimatedDistance { get; set; }

    }
}
