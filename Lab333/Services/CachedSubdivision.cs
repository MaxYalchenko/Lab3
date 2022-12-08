using Lab333.Data;
using Lab333.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab333.Services
{
    public class CachedSubdivision : ICached<Subdivision>
    {
        private readonly Context _context;
        private readonly IMemoryCache _memoryCache;

        public CachedSubdivision(Context context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }
        public void AddList(string cacheKey, int rowsNumber = 17)
        {
            IEnumerable<Subdivision> subdivisions = _context.Subdivisions.ToList();
            if (subdivisions.Any())
            {
                _memoryCache.Set(cacheKey, subdivisions, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(256)
                });
            }
        }

        public IEnumerable<Subdivision> GetList(int rowsNumber = 17)
        {
            return _context.Subdivisions.Take(rowsNumber).ToList();
        }

        public IEnumerable<Subdivision> GetList(string cacheKey, int rowsNumber = 17)
        {
            IEnumerable<Subdivision> subdivisions;
            if (!_memoryCache.TryGetValue(cacheKey, out subdivisions))
            {
                subdivisions = _context.Subdivisions.Take(rowsNumber).ToList();
                if (subdivisions.Any())
                {
                    _memoryCache.Set(cacheKey, subdivisions, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(256)));
                }
            }
            return subdivisions;
        }
    }
}
