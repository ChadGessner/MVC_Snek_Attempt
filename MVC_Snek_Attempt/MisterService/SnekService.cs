using Microsoft.Extensions.Caching.Memory;
using MVC_Snek_Attempt.Data;
using System.Reflection;

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
        public List<int> SnekDirection(List<int> snek, Directions direction)
        {
            Console.WriteLine($"The snek: {String.Join("", snek)} || The Direction: {direction} ...");
            List<int> movedSnek = Move(snek, direction);
            if (snek[snek.Count - 1] == movedSnek[movedSnek.Count - 1])
            {
                Console.WriteLine("previous snek: " + snek[snek.Count - 1] + "current snek: " + movedSnek[movedSnek.Count - 1]);
                return _cache.MutateSnek(snek);
            }
            return _cache.MutateSnek(movedSnek);

        }
        private List<int> GetNewSnek()
        {
            int start = GameValues.Tengen - GameValues.SnekLength;
            List<int> oldSnek = _cache.GetSnek();
            List<int> newSnek = Enumerable
                .Range(start, GameValues.SnekLength)
                .ToList();
            return oldSnek.Count == 0 ? _cache.MutateSnek(newSnek) : oldSnek; 
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

        private List<int> Move(List<int> snek, Directions currentDirection)
        {
            int headIndex = snek.Count() - 1;
            int headValue = snek[headIndex] += GameValues.DefaultDirections[currentDirection];
            if (GameValues.BorderValues.Contains(headValue) || snek.Contains(headValue))
            {
                return snek;
            }
            IEnumerable<int> head = new List<int>() { headValue };
            if(GameValues.SnekLength + _cache.GetGameScore() == snek.Count())
            {
                return snek.Concat(head).ToList();
            }

            return snek.Skip(1).Concat(head).ToList();

        }
        
    }
}
