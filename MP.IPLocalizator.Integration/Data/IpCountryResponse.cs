namespace MP.IPLocalizator.Integration.Data
{
    public class IpCountryResponse
    {
        public string country_name { get; set; }
        public string Country_code { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public Location Location { get; set; }
    }
}
