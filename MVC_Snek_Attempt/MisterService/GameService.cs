namespace MVC_Snek_Attempt.MisterService
{
    public class GameService : IGameService
    {
        private readonly IGridService _gridService;
        private readonly ISnekService _snekService;
        public GameService(IGridService gridService, ISnekService snekService)
        {
            _gridService = gridService;
            _snekService = snekService;
        }
        public List<List<int>> GetGridFromService()
        {
            var grid = _gridService.GetGrid();
            
            return grid;
        }
        
    }
}
