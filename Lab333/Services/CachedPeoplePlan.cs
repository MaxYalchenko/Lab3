using Lab333.Data;
using Lab333.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab333.Services
{
    public class CachedPeoplePlan : ICached<PeoplePlan>
    {
        private readonly Context _context;
        private readonly IMemoryCache _memoryCache;

        public CachedPeoplePlan(Context context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }
        public void AddList(string cacheKey, int rowsNumber = 17)
        {
            IEnumerable<PeoplePlan> peoplePlans = _context.PeoplePlans.ToList();
            if (peoplePlans.Any())
            {
                _memoryCache.Set(cacheKey, peoplePlans, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(256)
                });
            }
        }

        public IEnumerable<PeoplePlan> GetList(int rowsNumber = 17)
        {
            return _context.PeoplePlans.Take(rowsNumber).ToList();
        }

        public IEnumerable<PeoplePlan> GetList(string cacheKey, int rowsNumber = 17)
        {
            IEnumerable<PeoplePlan> peoplePlans;
            if (!_memoryCache.TryGetValue(cacheKey, out peoplePlans))
            {
                peoplePlans = _context.PeoplePlans.Take(rowsNumber).ToList();
                if (peoplePlans.Any())
                {
                    _memoryCache.Set(cacheKey, peoplePlans, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(256)));
                }
            }
            return peoplePlans;
        }
    }
}
