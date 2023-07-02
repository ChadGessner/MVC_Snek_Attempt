
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
            _grid = GenerateGrid();
            Console.WriteLine("The grid service has been initialized");
        }
        
        public List<List<int>> GetGrid() 
        {
            
            List<List<int>> newGrid = new List<List<int>>();
            IEnumerable<int> snek = GetSnekValues();
            if(snek == null || snek.Count() == 0)
            {
                return _grid;
            }
            for (int y = 0; y < _size; y++)
            {
                newGrid.Add(new List<int>());
                for(int x = 0; x < _size; x++)
                {
                    if (_grid.Count() != 0 && snek.Contains(_grid[y][x]))
                    {
                        newGrid[y].Add(GameValues.SnekValue);
                        continue;
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
        private List<List<int>> GenerateGrid()
        {
            List<List<int>> grid = new List<List<int>>();
            for (int y = 0; y < _size; y++)
            {
                grid.Add(new List<int>());
                for (int x = 0; x < _size; x++)
                {
                    grid[y].Add((_size * y) + (x + 1));
                }
            }
            Console.WriteLine(String.Join("", grid.Select(x => String.Join("", x))));
            return grid;
        }
    }
}
