using Microsoft.Extensions.Caching.Memory;
using MVC_Snek_Attempt.Data;

namespace MVC_Snek_Attempt.MisterService
{
    public class SnekService : ISnekService
    {
        private int snekLength { get; set; }
        private int snekStartPosition { get; set; }
        private int snekOffset { get; set; }
        private directions currentDirection { get; set; }
        private ICachingService _cache { get; set; }
        public SnekService(ICachingService cache)
        {
            snekLength = GameValues.SnekLength;
            snekStartPosition = GameValues.Tengen;
            snekOffset = 0;
            _cache = cache;
        }
        public void SetDirection(directions direction)
        {
            currentDirection = direction;
        }
        private List<int> GetNewSnek()
        {
            int start = GameValues.Tengen - GameValues.SnekLength;
            
            List<int> newSnek = Enumerable
                .Range(start, GameValues.SnekLength)
                .ToList();
            _cache.MutateSnek(newSnek);
            
            return _cache.GetSnek();
        }
        public void SnekDirection(List<int> snek, directions direction)
        {
            switch(direction)
            {
                case directions.up:
                    snek.Select(x => x + GameValues.GridLength);
                    break;
                case directions.down:
                    snek.Select(x => x - GameValues.GridLength);
                    break;
                case directions.left:
                    snek.Select(x => x - 1);
                    break;
                case directions.right:
                    snek.Select(x => x + 1);
                    break;
                default:
                    break;
            }
            _cache.MutateSnek(snek);
        }
        private List<int> SnekMotion()
        {
            List<int> snek = _cache.GetSnek();
            directions currentDirection = _cache.GetDirection();
            if(snek.Count() != 0)
            {

                SnekDirection(snek, currentDirection);
            }
            else
            {
                _cache.MutateSnek(GetNewSnek());
            }
            return _cache.GetSnek();
        }
        public List<int> Triggered()
        {
           var snek = SnekMotion();
            return snek;
        }
    }
}
