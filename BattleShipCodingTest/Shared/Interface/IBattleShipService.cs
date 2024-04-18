namespace BattleShipCodingTest.Shared.Interface
{
    public interface IBattleShipService
    {        
        int GetBoardSize();
        void SetBoardSize(int boardSize);
        int GetTotalShips();
        int GetTotalShipsDestroyed();
        string GetMatrixToDisplay();
        void CreateBoard(int value);
        void AddShips(int totalShips);
        void RestartGame();
        bool Fire(string position);
    }
}
