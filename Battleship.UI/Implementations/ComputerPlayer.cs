using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Battleship.UI.GameLogic;
using Battleship.UI.InputManagement;
using Battleship.UI.Interfaces;

namespace Battleship.UI.Implementations
{
    public class ComputerPlayer : IPlayer
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
            string[] avaliableColums = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            Random rng = new Random();
            string column;
            int row;
            string coord;
            bool isDuplicate = false;
            do
            {
                isDuplicate = false;
                column = avaliableColums[rng.Next(0, 10)];
                row = rng.Next(1, 11);
                coord = column + row;
                for (int i = 0; i < shotsTaken.Length; i++)
                {
                    if (coord == shotsTaken[i])
                    {
                        isDuplicate = true;
                        break;
                    }
                }

                if(!isDuplicate)
                {
                    isDuplicate = true;
                    break; 
                }
            } while (true);
            ConsoleIO.GetValidMove(opp, oppShips, this);
            return coord;
        }

        public string[] PlaceShips(int shipNumber)
        {
            //Place ship 
            string[] avaliableColums = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O" };
            string[] selectedAnswer = new string[PlayerShips[shipNumber].Size];
            string direction = "";
            string column = "";
            int row = 0;
            Random rng = new Random();
            column = avaliableColums[rng.Next(0, 11)];
            row = rng.Next(1, 11);
            if(rng.Next(1,3) == 1)
            {
                direction = "Vertical";
            }
            else
            {
                direction = "Horizontal";
            }
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
