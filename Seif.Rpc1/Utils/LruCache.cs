using System.Collections.Concurrent;
using System.Linq;

namespace Seif.Rpc.Utils
{
    public class LruCache
    {
        private static readonly ConcurrentDictionary<string, CacheItem> Cache = new ConcurrentDictionary<string, CacheItem>();

        public LruCache(int capacity = 100)
        {
            Capacity = capacity;
        }
        public int Capacity { get; private set; }

        public string Get(string key)
        {
            CacheItem val;

            if (Cache.TryGetValue(key, out val))
            {
                val.UsedTimes ++;
                return val.Value;
            }

            return string.Empty;
        }

        public bool Exists(string key)
        {
            return Cache.ContainsKey(key);
        }

        public void Add(string key, string val)
        {
            // Check Capacity
            if (Cache.Count > Capacity)
            {
                RemoveLruItems();
            }

            Cache.TryAdd(key, new CacheItem { Value = val, UsedTimes = 0 });
        }

        private void RemoveLruItems(int removeCount = 5)
        {
            var lruItems = Cache.OrderBy(p => p.Value.UsedTimes).Take(removeCount);
            foreach (var item in lruItems)
            {
                if (item.Value != null)
                {
                    CacheItem val;
                    Cache.TryRemove(item.Key, out val);
                }
            }
        }

        private class CacheItem
        {
            public string Value { get; set; }
            public int UsedTimes { get; set; }
        }
    }


}