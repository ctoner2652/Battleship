using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.UI.GameLogic;
using Battleship.UI.InputManagement;
using Battleship.UI.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Battleship.UI.Implementations
{
    public class HumanPlayer : IPlayer
    {
        public Ship[] PlayerShips { get; set; } =
        [
            new Ship { Name = "Aircraft Carrier (A)", Size = 5 },
            new Ship { Name = "Battleship (B)", Size = 4 },
            new Ship { Name = "Cruiser (C)", Size = 3 },
            new Ship { Name = "Submarine (S)", Size = 3 },
            new Ship { Name = "Destroyer (D)", Size = 2 }
        ];

        public string lastShot { get; set; }
        public string Name { get; set; }
        public string[] shotsTaken { get; set; } = new string[100];
        public int turnNumber { get; set; }
        public bool isHuman { get; set; }

        public string getMove(IPlayer opp, Ship[] oppShips)
        {
            return ConsoleIO.GetValidMove(opp, oppShips, this);
        }

        public string[] PlaceShips(int shipNumber)
        {
            //Place ship 
            string[] avaliableColums = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O" };
            string[] selectedAnswer = new string[PlayerShips[shipNumber].Size];
            string cord = "";
            string direction = "";
            string column = "";
            int row = 0;
            cord = ConsoleIO.GetCoordinateStart(PlayerShips[shipNumber], PlayerShips);
            direction = ConsoleIO.GetDirection();
            column = cord.Substring(0, 1);
            row = int.Parse(cord.Substring(1));
            if (direction == "Vertical")
            {
                for (int i = 0; i < PlayerShips[shipNumber].Size; i++)
                {
                    selectedAnswer[i] = $"{column}{row + i}";
                }
            }
            else
            {
                int startingPosition = 0;
                for (int i = 0; i < 10; i++)
                {
                    if (column == avaliableColums[i])
                    {
                        startingPosition = i;
                        break;
                    }
                }
                for (int i = 0; i < PlayerShips[shipNumber].Size; i++)
                {
                    selectedAnswer[i] = $"{avaliableColums[startingPosition + i]}{row}";

                }
            }
            return selectedAnswer;
        }
    }
}

