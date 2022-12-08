using System.Collections.Generic;

namespace Lab333.Services
{
    public interface ICached<T>
    {
        public IEnumerable<T> GetList(int rowsNumber = 17);
        public void AddList(string cacheKey, int rowsNumber = 17);
        public IEnumerable<T> GetList(string cacheKey, int rowsNumber = 17);
    }
}
