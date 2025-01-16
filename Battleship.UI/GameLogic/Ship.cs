using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.UI.GameLogic
{
    public class Ship
    {
        public string Name {  get; set; }
        public int Size {  get; set; }
        public string[] ShipCoordinates { get; set; } = ["", "", "", "", ""];
        public bool isSunk = false;
    }
}
