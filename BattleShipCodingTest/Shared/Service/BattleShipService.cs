using BattleShipCodingTest.Shared.Exceptions;
using BattleShipCodingTest.Shared.Interface;
using BattleShipCodingTest.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShipCodingTest.Shared.Service
{
    public class BattleShipService : IBattleShipService
    {
        private int boardSize;
        private string[,] shipHit;
        private string[,] board;
        private List<Ship> ships = new List<Ship>();
        private List<string> destroyedShips = new List<string>();
        private int totalShipsDestroyed;
        private string matrixToDisplay;
        private char position = 'A';
        private Random random = new Random();

        public void CreateBoard(int value)
        {
            boardSize = value;
            InitializeBoard();
            UpdateBoard();
        }

        public int GetBoardSize() => boardSize;
        public int GetTotalShips() => ships.Count;
        public int GetTotalShipsDestroyed() => totalShipsDestroyed;

        public int TotalShips() => ships.Count;

        public int TotalShipsDestroyed() => totalShipsDestroyed;

        public string MatrixToDisplay => matrixToDisplay;
        public string GetMatrixToDisplay() => matrixToDisplay;

        public void AddShips(int totalShips)
        {
            for (int i = 0; i < totalShips; i++)
            {
                int shipSize = random.Next(2, boardSize);
                Ship newShip;
                do
                {
                    newShip = GenerateRandomShip(shipSize);
                } while (IsOverlapping(newShip));

                ships.Add(newShip);
                AddShipOnBoard(newShip);
            }
            UpdateBoard();
        }

        public void RestartGame()
        {
            boardSize = 0;
            ships.Clear();
            destroyedShips.Clear();
            matrixToDisplay = "";
            position = 'A';
            shipHit = null;
            board = null;
            totalShipsDestroyed = 0;
            matrixToDisplay = null;
        }

        public void SetBoardSize(int boardSize)
        {
            this.boardSize = boardSize;
            InitializeBoard();
            UpdateBoard();
        }

        public bool Fire(string value)
        {
            bool validCoordinate = false;
            position = 'A';
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (j.ToString() + position.ToString() == value.ToUpper())
                    {
                        var x = i;
                        var y = j;
                        if (shipHit[x, y].Contains("S"))
                        {
                            if (HandleHit(x, y))
                                return true;
                            else
                                return false;
                        }
                        else
                        {
                            HandleMiss(x, y);
                            return false;
                        }
                        validCoordinate = true;
                    }
                }
                position++;
            }

            if (!validCoordinate)
            {
                throw new BattleShipApiException("Please Enter Valid Coordinate");
            }
            return false;
        }

        private void InitializeBoard()
        {
            shipHit = new string[boardSize, boardSize];
            board = new string[boardSize, boardSize];
            position = 'A';
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    shipHit[i, j] = $"[{j}{position}]";
                    board[i, j] = $"[{j}{position}]";
                }
                position++;
            }
        }

        private Ship GenerateRandomShip(int shipSize)
        {
            Ship ship = new Ship("S" + (ships.Count + 1));
            bool isHorizontal = random.Next(2) == 0;
            int x = random.Next(0, boardSize);
            int y = random.Next(0, boardSize);

            if (isHorizontal)
            {
                while (x + shipSize > boardSize)
                {
                    x = random.Next(0, boardSize - shipSize);
                }
            }
            else
            {
                while (y + shipSize > boardSize)
                {
                    y = random.Next(0, boardSize - shipSize);
                }
            }

            for (int i = 0; i < shipSize; i++)
            {
                if (isHorizontal)
                {
                    AddPosition(ship, x + i, y);
                }
                else
                {
                    AddPosition(ship, x, y + i);
                }
            }

            return ship;
        }

        private void AddPosition(Ship ship, int x, int y) => ship.Positions.Add((x, y));

        private void AddShipOnBoard(Ship newShip)
        {
            foreach (var position in newShip.Positions)
            {
                shipHit[position.Item1, position.Item2] = $"[{newShip.Name}]";
            }
        }

        private void UpdateBoard()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (destroyedShips.Contains(board[i, j]))
                    {
                        builder.Append(board[i, j] + " ");
                    }
                    else if (shipHit[i, j].Contains("XX"))
                    {
                        builder.Append(shipHit[i, j] + " ");
                    }
                    else
                    {
                        builder.Append(board[i, j] + " ");
                    }
                }
                builder.AppendLine();
            }
            matrixToDisplay = builder.ToString();
        }

        private bool HandleHit(int x, int y)
        {
            foreach (var ship in ships)
            {
                if (ship.Positions.Contains((x, y)))
                {
                    shipHit[x, y] = shipHit[x, y];
                    ship.IsDestroyed = CheckShipDestroyed(ship);
                    if (ship.IsDestroyed)
                    {
                        totalShipsDestroyed++;
                        board[x, y] = shipHit[x, y];
                        shipHit[x, y] = "[XX]";
                        destroyedShips.Add("[" + ship.Name + "]");
                        UpdateBoard();
                        if (TotalShips == TotalShipsDestroyed)
                        {
                            Console.WriteLine($"\n\u001b[37m ***********************************************");
                            Console.WriteLine($"\u001b[37m Congratulations! You destroyed all ships. you won.");
                            Console.WriteLine($"\u001b[37m *************************************************");
                        }
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"\n\u001b[37m You Miss the target");
                        board[x, y] = shipHit[x, y];
                        shipHit[x, y] = "[XX]";
                        UpdateBoard();
                        return false;
                    }
                }
            }
            return false;
        }

        private void HandleMiss(int x, int y)
        {
            Console.WriteLine($"\n\u001b[37m You Miss the target");
            shipHit[x, y] = "[XX]";
            UpdateBoard();
        }

        private bool CheckShipDestroyed(Ship ship)
        {
            bool isShipDestroyed = false;
            foreach (var position in ship.Positions)
            {
                var (x, y) = position;
                if (!shipHit[x, y].Contains($"[{ship.Name}]"))
                {
                    isShipDestroyed = true;
                }
            }
            if (isShipDestroyed)
            {
                Console.WriteLine($"\n\u001b[37m You Have Destroyed this ship:-{ship.Name}");
                return true;
            }
            return false;
        }

        private bool IsOverlapping(Ship newShip)
        {
            foreach (var existingShip in ships)
            {
                foreach (var position in existingShip.Positions)
                {
                    if (newShip.Positions.Contains(position))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
