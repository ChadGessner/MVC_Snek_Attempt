namespace MVC_Snek_Attempt.Data
{
    public static class GameValues
    {
        public const int SnekLength = 4;
        public const int GridLength = 40;
        public const int GridMaxValue = GridLength * GridLength;
        public const int Tengen = GridLength / 2 * GridLength;
        public const int GridValue = 0;
        public const int SnekValue = 1;
        public const int AppleValue = 2;
        public const int WallValue = 3;
        public static readonly IDictionary<int, int> keyKeyValuePairs;
        public static readonly List<List<int>> DefaultGridValues;
        public static readonly IDictionary<Directions, int> DefaultDirections;
        public static readonly List<int> BorderValues;
        static GameValues()
        {
            BorderValues = GetBorderValues();
            keyKeyValuePairs = new Dictionary<int, int>()
            {
                {
                    87 , 0
                },
                {
                    83, 1
                },
                {
                    65, 2
                },
                {
                    68, 3
                }
            };
            DefaultGridValues = GenerateGrid();
            DefaultDirections = new Dictionary<Directions, int>()
            {
                { 
                    (Directions)0,
                    -GridLength 
                },{
                    (Directions)1,
                     GridLength
                },{
                    (Directions)2,
                    -1
                },{
                    (Directions)3,
                     1
                },

            };
        }
        private static List<List<int>> GenerateGrid()
        {
            
            //List<List<int>> grid = new List<List<int>>();
            //for (int y = 0; y < GridLength; y++)
            //{
            //    grid.Add(new List<int>());
            //    for (int x = 0; x < GridLength; x++)
            //    {
            //        grid[y].Add((GridLength * y) + (x + 1));
            //    }
            //}

            return Enumerable.Range(0, GridLength)
                .Select(y => Enumerable
                .Range(0, GridLength)
                .Select(x => GridLength * y + (x + 1))
                .ToList())
                .ToList(); 
        }
        private static List<int> GetBorderValues()
        {
            //q <= GridLength || q % GridLength == 0 || q % (GridLength + 1) == 0 || 
            return Enumerable.Range(0, GridMaxValue)
                .Where(
                q => q % GridLength == 0 || (q > (GridMaxValue - GridLength) && q < GridMaxValue ))
                .ToList();
        }
    }
    
}
