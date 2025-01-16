using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.UI.GameLogic;

namespace Battleship.UI.Interfaces
{
    public interface IPlayer
    {
         Ship[] PlayerShips { get; set; }
         string[] shotsTaken { get; set; }
         int turnNumber { get; set; } 
         string Name { get; set; }
         string lastShot { get; set; }
         string[] PlaceShips(int shipNumber);
         string getMove(IPlayer opp, Ship[] oppShips);
         bool isHuman { get; set; }
         
    }
}
