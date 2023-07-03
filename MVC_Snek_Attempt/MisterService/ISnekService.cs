using MVC_Snek_Attempt.Data;

namespace MVC_Snek_Attempt.MisterService
{
    public interface ISnekService
    {
        List<int> Triggered();
        void SetDirection(Directions direction);
        Directions GetCurrentDirection();
    }
}