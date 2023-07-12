namespace MVC_Snek_Attempt.Data
{
    public static class GameValues
    {
        //strings
        public const string currentDirection = "currentDirection";
        public const string apple = "apple";
        public const string score = "score";
        public const string grid = "grid";
        public const string snek = "snek";
        public const string status = "status";

        //array of cache keys
        public static readonly string[] CacheKeys;

        // integers
        public const int SnekLength = 4;
        public const int GridLength = 40;
        public const int GridMaxValue = GridLength * GridLength;
        public const int Tengen = ((GridLength / 2) * GridLength) - (GridLength / 2);
        public const int GridValue = 0;
        public const int SnekValue = 1;
        public const int AppleValue = 2;
        public const int WallValue = 3;

        //collections
        public static readonly IDictionary<int, int> keyKeyValuePairs;
        public static readonly List<List<int>> DefaultGridValues;
        public static readonly IDictionary<Directions, int> DefaultDirections;
        public static readonly List<int> BorderValues;
        static GameValues()
        {
            DefaultGridValues = GenerateGrid();

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

            CacheKeys = new string[6]
            {
                currentDirection,
                score,
                snek,
                grid,
                apple,
                status
            };
            
        }
        private static List<List<int>> GenerateGrid()
        {
            
            return Enumerable.Range(0, GridLength)
                .Select(y => Enumerable
                .Range(0, GridLength)
                .Select(x => GridLength * y + (x + 1))
                .ToList())
                .ToList(); 
        }
        private static List<int> GetBorderValues()
        {
            var topWall = DefaultGridValues.First();

            var bottomWall = DefaultGridValues.Last();

            var leftWall = Enumerable
                .Range(0, GridLength)
                .Select(y => DefaultGridValues[y].First());

            var rightWall = Enumerable
                .Range(0, GridLength)
                .Select(y => DefaultGridValues[y].Last());

            return topWall
                .Concat(bottomWall)
                .Concat(leftWall)
                .Concat(rightWall)
                .ToList();

        }
    }
}
