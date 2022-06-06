using System.Collections.Generic;

namespace MP.IPLocalizator.Integration.Data
{
    public class RestCountriesResponse
    {
        public List<string> Timezones { get; set; }

        public CountryNameTranslations Translations { get; set; }
    }
}
