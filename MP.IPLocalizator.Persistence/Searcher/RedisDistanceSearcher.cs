using MP.IPLocalizator.Business.Data;
using MP.IPLocalizator.Business.Searchers;
using MP.IPLocalizator.Persistence.Constanst;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace MP.IPLocalizator.Persistence.Searcher
{
    public class RedisDistanceSearcher : IDistanceSearcher
    {
        private IDatabase redisDatabase;

        public RedisDistanceSearcher(IConnectionMultiplexer connectionMultiplexer)
        {
            this.redisDatabase = connectionMultiplexer.GetDatabase();
        }

        public AverageDistanceData GetAverageDistance()
        {
            var averageDistance = this.GetRedisValue(RedisConstants.AverageDistanceKey);
            if (averageDistance.HasValue)
            {
                return JsonConvert.DeserializeObject<AverageDistanceData>(averageDistance);
            }
            else
            {
                return null;
            }
        }

        public double? GetLongestDistance()
        {
            var longestDistance = this.GetRedisValue(RedisConstants.LongestDistanceKey);

            if (longestDistance.HasValue)
            {
                return JsonConvert.DeserializeObject<double?>(longestDistance);
            }
            else
            {
                return null;
            }
        }

        public double? GetShortestDistance()
        {
            var shortestDistance = this.GetRedisValue(RedisConstants.ShortestDistanceKey);
            if (shortestDistance.HasValue)
            {
                return JsonConvert.DeserializeObject<double?>(shortestDistance);
            }
            else
            {
                return null;
            }
        }

        private RedisValue GetRedisValue(string key)
        {
            return this.redisDatabase.HashGet(RedisConstants.StreamName, key);
        }
    }
}
