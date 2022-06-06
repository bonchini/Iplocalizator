namespace MP.IPLocalizator.Integration.Data
{
    public abstract class RestCredentials
    {
        public string Url { get; set; }

        public string Route { get; set; }

        public string PrivateKey { get; set; }
    }
}
