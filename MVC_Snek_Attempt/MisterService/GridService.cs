
using MVC_Snek_Attempt.Data;

namespace MVC_Snek_Attempt.MisterService
{
    public class GridService : IGridService
    {
        private int _size { get; set; } = GameValues.GridLength;
        private  List<List<int>> _grid 
        {
            get;
            set;
        }
        private readonly ICachingService _cache;
        public GridService(ICachingService cache)
        {
            _cache = cache;
            _grid = GameValues.DefaultGridValues;
            Console.WriteLine("The grid service has been initialized");
        }
        public int GetGameScore()
        {
            return _cache.GetGameScore();
        }
        //public int GetIndexOfSnekHead(List<int> snek)
        //{
        //    int head = snek[snek.Count - 1];


        //}
        public List<List<int>> GetGrid() 
        {
            
            List<List<int>> newGrid = new List<List<int>>();
            List<int> snek = GetSnekValues();
            int snekHead = snek[snek.Count - 1];
            if (GameValues.BorderValues.Contains(snekHead))
            {
                Console.WriteLine("Game Over");
                return new List<List<int>>();
            }
            int apple = GenerateApple(snek);
            
            if (snek == null || snek.Count() == 0)
            {
                return _grid;
            }
            for (int y = 0; y < _size; y++)
            {
                int appleIndexQuery = _grid[y].IndexOf(apple);
                newGrid.Add(new List<int>());
                for(int x = 0; x < _size; x++)
                {
                    if (_grid.Count() != 0 && snek.Contains(_grid[y][x]))
                    {
                        newGrid[y].Add(GameValues.SnekValue);
                        continue;
                    }
                    if(_grid.Count() != 0 && appleIndexQuery == x)
                    {
                        newGrid[y].Add(GameValues.AppleValue);
                        continue;
                    }
                    if (GameValues.BorderValues.Contains(_grid[y][x]))
                    {
                        newGrid[y].Add(GameValues.WallValue);
                    }
                    
                    newGrid[y].Add(GameValues.GridValue);
                }
            }
            _cache.MutateGrid(newGrid);
            return _cache.GetGrid(); 
        }
        private List<int> GetSnekValues()
        {
            
            List<int> snek = _cache.GetSnek();
            
            return snek;
        }
        private int GenerateApple(List<int> snek)
        {
            int apple = _cache.GetApple();
            if (snek.Contains(apple) || apple == -1)
            {
                _cache.SetGameScore();
                while (true)
                {

                    int randInt = new Random().Next(0, GameValues.GridMaxValue);
                    if (snek.Contains(randInt) || GameValues.BorderValues.Contains(randInt))
                    {
                        continue;
                    }
                    _cache.SetApple(randInt);

                    return randInt;
                }
                
            }
            return apple;
        }
    }
}
