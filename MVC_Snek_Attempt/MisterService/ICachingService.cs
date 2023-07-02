using MVC_Snek_Attempt.Data;

namespace MVC_Snek_Attempt.MisterService
{
    public interface ICachingService
    {
        List<int> GetSnek();
        List<int> MutateSnek(List<int> snek);
        List<List<int>> MutateGrid(List<List<int>> grid);
        List<List<int>> GetGrid();
        Directions GetDirection();

    }
}