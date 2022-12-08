using Lab333.Data;
using Lab333.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab333.Services
{
    public class CachedWorkpeople : ICached<Workpeople>
    {
        private readonly Context _context;
        private readonly IMemoryCache _memoryCache;

        public CachedWorkpeople(Context context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }
        public void AddList(string cacheKey, int rowsNumber = 17)
        {
            IEnumerable<Workpeople> workpeoples = _context.Workpeoples.ToList();
            if (workpeoples.Any())
            {
                _memoryCache.Set(cacheKey, workpeoples, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(256)
                });
            }
        }

        public IEnumerable<Workpeople> GetList(int rowsNumber = 17)
        {
            return _context.Workpeoples.Take(rowsNumber).ToList();
        }

        public IEnumerable<Workpeople> GetList(string cacheKey, int rowsNumber = 17)
        {
            IEnumerable<Workpeople> workpeoples;
            if (!_memoryCache.TryGetValue(cacheKey, out workpeoples))
            {
                workpeoples = _context.Workpeoples.Take(rowsNumber).ToList();
                if (workpeoples.Any())
                {
                    _memoryCache.Set(cacheKey, workpeoples, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(256)));
                }
            }
            return workpeoples;
        }
    }
}
