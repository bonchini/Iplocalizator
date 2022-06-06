using MP.IPLocalizator.Business.Data;

namespace MP.IPLocalizator.Business.Factories
{
    public static class DistanceMetricsResponseFactory
    {
        public static DistanceMetricsResponse Create(double? longestDistance, double? shortestDistance, AverageDistanceData averageDistance)
        {
            return new DistanceMetricsResponse()
            {
                ShortestDistance = shortestDistance,
                LongestDistance = longestDistance,
                AverageDistance = averageDistance?.Value
            };
        }
    }
}
