﻿namespace MVC_Snek_Attempt.MisterService
{
    public interface IGameService
    {
        List<List<int>> UpdateAndGetGrid();
        
        void SetSnekDirectionOnKeyPress(int keyPress);
        int GetScore();
        bool GetGameStatus();
        void ResetCache();
    }
}