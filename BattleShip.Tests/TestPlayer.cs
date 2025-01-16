using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.UI.GameLogic;
using Battleship.UI.Interfaces;

namespace BattleShip.Tests
{
    public class TestPlayer : IPlayer
    {
        public Ship[] PlayerShips { get; set; } =
        [
            new Ship { Name = "Aircraft Carrier (A)", Size = 5 },
            new Ship { Name = "Battleship (B)", Size = 4 },
            new Ship { Name = "Cruiser (C)", Size = 3 },
            new Ship { Name = "Submarine (S)", Size = 3 },
            new Ship { Name = "Destroyer (D)", Size = 2 }
        ];
        public string[] shotsTaken { get; set; } = new string[100];
        public int turnNumber { get; set; }
        public string Name { get; set; }
        public string lastShot { get ; set ; }
        public string ManualMove { get; set; }
        public bool isHuman { get; set; }

        public string getMove(IPlayer opp, Ship[] oppShips)
        {
            return ManualMove;
        }

        public string[] PlaceShips(int shipNumber)
        {
            throw new NotImplementedException();
        }
    }
}
