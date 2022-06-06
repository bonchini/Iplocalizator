using MP.IPLocalizator.Business.Data;

namespace MP.IPLocalizator.Business.Searchers
{
    public interface IDistanceSearcher
    {
        public AverageDistanceData GetAverageDistance();
        public double? GetLongestDistance();
        public double? GetShortestDistance();
    }
}
