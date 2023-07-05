using MVC_Snek_Attempt.Data;

namespace MVC_Snek_Attempt.MisterService
{
    public interface ICachingService
    {
        List<int> GetSnek();
        List<int> MutateSnek(List<int> snek);
        List<List<int>> MutateGrid(List<List<int>> grid);
        List<List<int>> GetGrid();
        Directions SetDirection(Directions direction);
        Directions GetDirection();
        int GetApple();
        int SetApple(int apple);
        int GetGameScore();
        int SetGameScore();

    }
}