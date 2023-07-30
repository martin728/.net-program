using System;
using System.Collections.Generic;

namespace Cabinet
{
    public class CacheManager
    {
        private Dictionary<Type, List<IDocumentCard>> cache;
        private Dictionary<Type, TimeSpan> cacheExpirationTimes;

        public CacheManager()
        {
            cache = new Dictionary<Type, List<IDocumentCard>>();
            cacheExpirationTimes = new Dictionary<Type, TimeSpan>();
        }

        public void AddToCache(Type type, List<IDocumentCard> cards, TimeSpan expirationTime)
        {
            cache[type] = cards;
            cacheExpirationTimes[type] = expirationTime;
        }

        public List<IDocumentCard> GetFromCache(Type type)
        {
            if (cache.TryGetValue(type, out var cards))
            {
                return cards;
            }

            return null;
        }

        public TimeSpan GetCacheExpirationTime(Type type)
        {
            if (cacheExpirationTimes.TryGetValue(type, out var expirationTime))
            {
                return expirationTime;
            }

            return TimeSpan.Zero;
        }
    }
}