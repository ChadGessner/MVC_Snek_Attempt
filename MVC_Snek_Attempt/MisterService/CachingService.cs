using Microsoft.Extensions.Caching.Memory;
using MVC_Snek_Attempt.Data;

namespace MVC_Snek_Attempt.MisterService
{
    public class CachingService : ICachingService
    {
        private readonly IMemoryCache _cache;
        public CachingService(IMemoryCache cache)
        {
            _cache = cache;
            
        }
        public List<int> MutateSnek(List<int> snek)
        {
            
            _cache.Set<List<int>>("snek", snek, TimeSpan.FromSeconds(3));
            
            return snek;
        }
        public List<int> GetSnek()
        {
            List<int> snek =  _cache.Get<List<int>>("snek");
            if (snek != null)
            {
                
                return snek;
            }
            return new List<int>();
        }
        public List<List<int>> GetGrid()
        {
            try
            {
                List<List<int>> grid = _cache.Get<List<List<int>>>("grid")!;
                
                if (grid != null)
                {
                    return grid;
                }
                return new List<List<int>>();
            }
            catch(Exception ex)
            {
                return new List<List<int>>();
            }
            
            
        }
        public List<List<int>> MutateGrid(List<List<int>> grid)
        {
            _cache.Set<List<List<int>>>("grid", grid, TimeSpan.FromSeconds(3));
            return grid;
        }
        public void SetDirection(directions direction)
        {
            _cache.Set<directions>("currentDirection", direction, TimeSpan.FromSeconds(9001));
        }
        public directions GetDirection()
        {
            return _cache.Get<directions>("currentDirection");
        }
    }
}
