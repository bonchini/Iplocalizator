using MP.IPLocalizator.Business.Data;
using MP.IPLocalizator.Business.Factories;

namespace MP.IPLocalizator.Business.Searchers
{
    public class DistanceMetricsSearcher
    {
        private readonly IDistanceSearcher distanceSearcher;

        public DistanceMetricsSearcher(IDistanceSearcher distanceSearcher)
        {
            this.distanceSearcher = distanceSearcher;
        }

        public DistanceMetricsResponse GetDistances()
        {
            var shortestDistance = this.distanceSearcher.GetShortestDistance();
            var longestDistance = this.distanceSearcher.GetLongestDistance();
            var averageDistance = this.distanceSearcher.GetAverageDistance();

            return DistanceMetricsResponseFactory.Create(longestDistance, shortestDistance, averageDistance);
        }
    }
}
