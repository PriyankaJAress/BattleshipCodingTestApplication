using System.Collections.Generic;

namespace BattleShipCodingTest.Shared.Models
{
    public class Ship
    {
        public string Name { get; }
        public List<(int, int)> Positions { get; }
        public List<(int, int)> AttackPositions { get; }
        public bool IsDestroyed { get; set; }
        public Ship(string name)
        {
            Name = name;
            Positions = new List<(int, int)>();
            AttackPositions = new List<(int, int)>();
            IsDestroyed = false;
        }
    }
}
