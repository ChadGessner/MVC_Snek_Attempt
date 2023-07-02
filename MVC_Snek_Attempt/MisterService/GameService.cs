namespace MVC_Snek_Attempt.MisterService
{
    public class GameService : IGameService
    {
        private readonly IGridService _gridService;
        private readonly ICachingService _cache;
        public GameService(IGridService gridService, ICachingService cache)
        {
            _gridService = gridService;
            _cache = cache;
        }
        public List<List<int>> GetGridFromService()
        {
            var grid = _gridService.GetGrid();
            
            return grid;
        }
        
    }
}
