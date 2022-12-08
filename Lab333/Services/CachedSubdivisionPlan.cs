using Lab333.Data;
using Lab333.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab333.Services
{
    public class CachedSubdivisionPlan : ICached<SubdivisionPlan>
    {
        private readonly Context _context;
        private readonly IMemoryCache _memoryCache;

        public CachedSubdivisionPlan(Context context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }
        public void AddList(string cacheKey, int rowsNumber = 17)
        {
            IEnumerable<SubdivisionPlan> subdivisionPlans = _context.SubdivisionPlans.ToList();
            if (subdivisionPlans.Any())
            {
                _memoryCache.Set(cacheKey, subdivisionPlans, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(256)
                });
            }
        }

        public IEnumerable<SubdivisionPlan> GetList(int rowsNumber = 17)
        {
            return _context.SubdivisionPlans.Take(rowsNumber).ToList();
        }

        public IEnumerable<SubdivisionPlan> GetList(string cacheKey, int rowsNumber = 17)
        {
            IEnumerable<SubdivisionPlan> subdivisionPlans;
            if (!_memoryCache.TryGetValue(cacheKey, out subdivisionPlans))
            {
                subdivisionPlans = _context.SubdivisionPlans.Take(rowsNumber).ToList();
                if (subdivisionPlans.Any())
                {
                    _memoryCache.Set(cacheKey, subdivisionPlans, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(256)));
                }
            }
            return subdivisionPlans;
        }
    }
}
