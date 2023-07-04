using Microsoft.Extensions.Caching.Memory;
using MVC_Snek_Attempt.Data;

namespace MVC_Snek_Attempt.MisterService
{
    public class SnekService : ISnekService
    {
        private int snekLength { get; set; }
        private int snekStartPosition { get; set; }
        private int snekOffset { get; set; }
        private Directions currentDirection { get; set; }
        private ICachingService _cache { get; set; }
        public SnekService(ICachingService cache)
        {
            snekLength = GameValues.SnekLength;
            snekStartPosition = GameValues.Tengen;
            snekOffset = 0;
            _cache = cache;
        }
        public List<int> Triggered()
        {
            var snek = SnekMotion();
            return snek;
        }
        public void SetDirection(Directions direction)
        {
            _cache.SetDirection(direction);
        }

        public Directions GetCurrentDirection()
        {
            return _cache.GetDirection();
        }
        public void SnekDirection(List<int> snek, Directions direction)
        {
            switch (direction)
            {
                case Directions.up:
                    snek.Select(x => x + GameValues.GridLength);
                    break;
                case Directions.down:
                    snek.Select(x => x - GameValues.GridLength);
                    break;
                case Directions.left:
                    snek.Select(x => x - 1);
                    break;
                case Directions.right:
                    snek.Select(x => x + 1);
                    break;
                default:
                    break;
            }
            _cache.MutateSnek(snek);
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
        
        
        private List<int> SnekMotion()
        {
            List<int> snek = _cache.GetSnek();
            Directions currentDirection = _cache.GetDirection();
            
            if (snek.Count() != 0)
            {

                SnekDirection(snek, currentDirection);
                
            }
            else
            {
                _cache.MutateSnek(GetNewSnek());
            }
            return _cache.GetSnek();
        }
        
    }
}
