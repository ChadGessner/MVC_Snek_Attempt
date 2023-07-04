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
        public static readonly IDictionary<int, int> keyKeyValuePairs;
        public static readonly List<List<int>> DefaultGridValues;
        static GameValues()
        {
        keyKeyValuePairs = new Dictionary<int, int>()
            {
                {
                    87 , 0
                },
                {
                    83, 2
                },
                {
                    65, 3
                },
                {
                    68, 3
                }
            };
            DefaultGridValues = GenerateGrid();
        }
        private static List<List<int>> GenerateGrid()
        {
            List<List<int>> grid = new List<List<int>>();
            for (int y = 0; y < GridLength; y++)
            {
                grid.Add(new List<int>());
                for (int x = 0; x < GridLength; x++)
                {
                    grid[y].Add((GridLength * y) + (x + 1));
                }
            }

            return grid;
        }
    }
}
