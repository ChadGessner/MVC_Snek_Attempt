﻿using MVC_Snek_Attempt.Data;

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
        public void ResetCache()
        {
            _snekService.ResetCache();
        }
        public List<List<int>> UpdateAndGetGrid()
        {
            _snekService.Triggered();
            var grid = _gridService.GetGrid();
            
            return grid;
        }
        
        public void SetSnekDirectionOnKeyPress(int keyPress)
        {
            
            if (ValidateKeyPressForDirection(keyPress))
            {
                Directions updatedDirection = (Directions)GameValues.keyKeyValuePairs[keyPress];
                _snekService.SetDirection(updatedDirection);
            }
            
        }
        private bool ValidateKeyPressForDirection(int keyPress)
        {
            
            Directions updatedDirection = (Directions)GameValues.keyKeyValuePairs[keyPress];
            
            Directions currentDirection = _snekService.GetCurrentDirection();
            
            bool isSameDirection = updatedDirection != currentDirection;
           // return isSameDirection;
            switch (currentDirection)
            {

                case Directions.up:
                    return updatedDirection != Directions.down && isSameDirection;

                case Directions.down:
                    return updatedDirection != Directions.up && isSameDirection;

                case Directions.left:
                    return updatedDirection != Directions.right && isSameDirection;

                case Directions.right:
                    return updatedDirection != Directions.left && isSameDirection;

                default:
                    return false;

            };
        }
        public int GetScore()
        {

            return _gridService.GetGameScore();
        }
        public bool GetGameStatus()
        {
            return _snekService.GetGameStatus();
        }
    }
}
