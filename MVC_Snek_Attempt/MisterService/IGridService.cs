namespace MVC_Snek_Attempt.MisterService
{
    public interface IGridService
    {
        List<List<int>> GetGrid();
        int GetGameScore();
    }
}