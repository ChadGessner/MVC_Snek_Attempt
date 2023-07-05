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
            
            List<int> snek;
            if(_cache.TryGetValue("snek", out snek))
            {
                return snek;
            }
            return  new List<int>();
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

        public Directions SetDirection(Directions direction)
        {

            _cache.Set<Directions>(
                "currentDirection", direction, TimeSpan.FromSeconds(5));
            return _cache.Get<Directions>("currentDirection");

        }

        public Directions GetDirection()
        {
            Directions currentDirection;
            
            if(_cache.TryGetValue<Directions>(
                "currentDirection", out currentDirection))
            {
                return SetDirection(currentDirection);
            }
            return SetDirection(Directions.right);
        }
        public int GetApple()
        {
            int apple;
            if(_cache.TryGetValue("apple", out apple))
            {
                return SetApple(apple);
                
            }
            return -1;
        }
        public int SetApple(int apple)
        {
            _cache.Set<int>("apple", apple, TimeSpan.FromSeconds(5));
            
            return _cache.Get<int>("apple");
        }
        public int SetGameScore()
        {
            int score = 0;
            if(_cache.TryGetValue("score", out score))
            {
                score++;
            }
            _cache.Set<int>("score", score, TimeSpan.FromMinutes(5));
            return score;

        }
        public int GetGameScore()
        {

            int score = 0;
            if (_cache.TryGetValue("score", out score))
            {
                return score;
            }
            return score;
        }
    }
    
}
