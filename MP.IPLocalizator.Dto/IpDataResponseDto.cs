using System;

namespace MP.IPLocalizator.Dto
{
    public class IpDataResponseDto
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
