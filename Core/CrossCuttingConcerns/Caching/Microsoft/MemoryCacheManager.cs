using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        // Adapter Pattern
        IMemoryCache _memoryCache; // Microsoft's
        // Injection is in Core Module

        public MemoryCacheManager()
        {
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
            // Get the corresponding of its from Core Module
        }

        // A Cache has a key, value and duration on the system
        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(duration)); // TimeSpan: Represents a time interval
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key); // This usage is better because of providing to avoid Type Casting errors
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key, out _); // out _ is used for key control, no need data.
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public void RemoveByPattern(string pattern) // From Documentation
        { // Remove the Cache by the passesed pattern at run time, Reflection provides us manipulation on objects at run time
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance); // Find Entries Collection type of MemoryCache in Memory - .Net Documentation
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic; // Get Values of EntriesCollection dynamically
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            foreach (var cacheItem in cacheEntriesCollection)
            { // Search every Cache item in Cache Entries Collection
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }
            // Regex : Regular Expression
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList(); // Rule, Matching

            foreach (var key in keysToRemove)
            { // In the Cache data search the keys are matched with the value and remove them all
                _memoryCache.Remove(key); // Remove from memory
            }
        }
    }
}
/* Search MemCache,
 * Redis
 */