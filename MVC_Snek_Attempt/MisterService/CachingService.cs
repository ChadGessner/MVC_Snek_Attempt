using Microsoft.Extensions.Caching.Memory;
using MVC_Snek_Attempt.Data;
using System.Reflection;

namespace MVC_Snek_Attempt.MisterService
{
    public class CachingService : ICachingService
    {
        private readonly IMemoryCache _cache;
        public CachingService(IMemoryCache cache)
        {
            _cache = cache;
            
        }
        private void Dispose()
        {
            _cache.Dispose();
        }
        private bool IsGameOver(List<int> snek)
        {
            List<int> currentSnek = GetSnek();
            bool game = true;
            bool over = false;
            for(int i = 0; i < snek.Count; i++)
            {
                over = currentSnek[i] == snek[i];
                if(game != over)
                {
                    return game;
                }
            }
            return game != over;
        }
        public List<int> MutateSnek(List<int> snek)
        {
            //if (IsGameOver(snek))
            //{
            //    return new List<int> { 0 };
            //}
            _cache.Set<List<int>>(GameValues.snek, snek, TimeSpan.FromSeconds(3));
            
            return snek;
        }
        public List<int> GetSnek()
        {
            
            List<int> snek;
            if(_cache.TryGetValue(GameValues.snek, out snek))
            {
                return snek;
            }
            return  new List<int>();
        }
        public List<List<int>> GetGrid()
        {
            try
            {
                List<List<int>> grid = _cache.Get<List<List<int>>>(GameValues.grid)!;
                
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
            _cache.Set<List<List<int>>>(GameValues.grid, grid, TimeSpan.FromSeconds(3));
            return grid;
        }

        public Directions SetDirection(Directions direction)
        {

            _cache.Set<Directions>(
                GameValues.currentDirection, direction, TimeSpan.FromSeconds(5));
            return _cache.Get<Directions>(GameValues.currentDirection);

        }

        public Directions GetDirection()
        {
            Directions currentDirection;
            
            if(_cache.TryGetValue<Directions>(
                GameValues.currentDirection, out currentDirection))
            {
                return SetDirection(currentDirection);
            }
            return SetDirection(Directions.right);
        }
        public int GetApple()
        {
            int apple;
            if(_cache.TryGetValue(GameValues.apple, out apple))
            {
                return SetApple(apple);
                
            }
            return -1;
        }
        public int SetApple(int apple)
        {
            _cache.Set<int>(GameValues.apple, apple, TimeSpan.FromSeconds(5));
            
            return _cache.Get<int>(GameValues.apple);
        }
        public int SetGameScore()
        {
            int score = 0;
            if(_cache.TryGetValue(GameValues.score, out score))
            {
                score++;
            }
            _cache.Set<int>(GameValues.score, score, TimeSpan.FromMinutes(5));
            return score;

        }
        public int GetGameScore()
        {

            int score = 0;
            if (_cache.TryGetValue(GameValues.score, out score))
            {
                return score;
            }
            return score;
        }
    }
    
}
