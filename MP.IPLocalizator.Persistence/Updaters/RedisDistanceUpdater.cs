using MP.IPLocalizator.Business.Data;
using MP.IPLocalizator.Business.Searchers;
using MP.IPLocalizator.Business.Updaters;
using MP.IPLocalizator.Persistence.Constanst;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace MP.IPLocalizator.Persistence.Updaters

{
    public class RedisDistanceUpdater : IDistanceUpdater
    {
        private readonly IDatabase redisDatabase;
        private readonly IDistanceSearcher distanceSearcher;

        public RedisDistanceUpdater(IConnectionMultiplexer redisConnectionMultiplexer, IDistanceSearcher distanceSearcher)
        {
            this.redisDatabase = redisConnectionMultiplexer.GetDatabase();
            this.distanceSearcher = distanceSearcher;
        }

        public void AddOrUpdateDistance(double distance)
        {
            this.UpdateLongestDistance(distance);
            this.UpdateShortestDistance(distance);
            this.UpdateAverageFistance(distance);
        }

        private void InternalPublish(string payload, string key)
        {
            HashEntry[] entries = new HashEntry[1];
            entries[0] = new HashEntry(key, payload);
            this.redisDatabase.HashSet($"{RedisConstants.StreamName}", entries);
        }

        private void UpdateShortestDistance(double distance)
        {
            var shortestDistance = this.distanceSearcher.GetShortestDistance();
            if (shortestDistance == null || shortestDistance > distance)
            {
                var newShortestDistance = JsonConvert.SerializeObject(distance);
                this.InternalPublish(newShortestDistance, RedisConstants.ShortestDistanceKey);
            }
        }

        private void UpdateLongestDistance(double distance)
        {
            var longestDistance = this.distanceSearcher.GetLongestDistance();
            if (longestDistance == null || longestDistance < distance)
            {
                var newLongestDistance = JsonConvert.SerializeObject(distance);
                this.InternalPublish(newLongestDistance, RedisConstants.LongestDistanceKey);
            }
        }

        private void UpdateAverageFistance(double distance)
        {                
            var averageDistance = this.distanceSearcher.GetAverageDistance();

            AverageDistanceData newAverageDistanceData;

            if (averageDistance != null)
            {
                var newCount = averageDistance.Count + 1;
                var newValue = (averageDistance.Value * averageDistance.Count + distance) / newCount;

                newAverageDistanceData = new AverageDistanceData()
                {
                    Value = newValue,
                    Count = newCount
                };
            }
            else
            {
                newAverageDistanceData = new AverageDistanceData()
                {
                    Value = distance,
                    Count = 1
                };
            }

            var newAverageDistance = JsonConvert.SerializeObject(newAverageDistanceData);

            this.InternalPublish(newAverageDistance, RedisConstants.AverageDistanceKey);
        }
    }
}
