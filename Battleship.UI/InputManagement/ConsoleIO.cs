using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using Battleship.UI.GameLogic;
using Battleship.UI.Interfaces;

namespace Battleship.UI.InputManagement
{
    public class ConsoleIO
    {

        public static string GetCoordinateStart(Ship currentShip, Ship[] Playerships)
        {
            string[] avaliableColums = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J"};
            string Coordinate = "";
            do
            { 
                Console.WriteLine($"Ship to place: {currentShip.Name} | Size: {currentShip.Size}");
                Console.Write("Enter the starting coordinate (ex: A5): ");
                Coordinate = Console.ReadLine();
                if (string.IsNullOrEmpty(Coordinate))
                {
                    Console.WriteLine("Please enter a valid Coordinate");
                    continue;
                }
                string column = Coordinate.Substring(0, 1);
                
                if (!avaliableColums.Contains(column))
                {
                    Console.WriteLine("Please enter a valid Coordinate");
                    continue;
                }
                int row;
                if (!int.TryParse(Coordinate.Substring(1), out row))
                {
                    Console.WriteLine("Please enter a valid Coordinate");
                    continue;
                }
                if (row > 10 || row < 0)
                {
                    Console.WriteLine("Please enter a valid Coordinate");
                    continue;
                }
                bool isValidCoordinate = true;
                for (int k = 0; k < Playerships.Length; k++)
                {
                    for (int l = 0; l < Playerships[k].Size; l++)
                    {
                        if (Playerships[k].ShipCoordinates[l] == Coordinate)
                        {
                            Console.WriteLine("Coordinate is ontop of already placed ship");
                            isValidCoordinate = false;
                        }
                    }
                }
                if (!isValidCoordinate)
                {
                    continue;
                }
                break;
                } while (true);
            return Coordinate;
        }

        public static string GetDirection()
        {
            do
            {
                Console.Write("Place ship (V)ertical or (H)orizontal: ");
                string val = Console.ReadLine().ToUpper();
                if (val == "V")
                {
                    return "Vertical";
                }else if(val == "H")
                {
                    return "Horizontal";
                }
                else
                {
                    Console.WriteLine("Please enter a valid selection");
                }
            } while (true);
        }

        public static void DisplayGrid(Ship[] Playerships)
        {
            Console.Clear();
            string[] avaliableColums = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O" };

            for (int i = 0; i < 10; i++)
            {
                if(i == 0)
                {
                    Console.Write($"   {avaliableColums[i]} ");
                }
                else
                {
                    Console.Write($"{avaliableColums[i]} ");
                }    
            }
            Console.WriteLine();
            for (int i = 1; i < 11; i++)
            {
                if (i == 10)
                {
                    Console.Write($"{i} ");
                }
                else
                {
                    Console.Write($" {i} ");
                }
                
                for (int j = 1; j < 11; j++)
                {
                    string Coordinate = $"{avaliableColums[j - 1]}{i}";
                    bool isShip = false;
                    for (int k = 0; k < Playerships.Length; k++)
                    {
                        for (int l = 0; l < Playerships[k].Size; l++)
                        {
                            if (Playerships[k].ShipCoordinates[l] == Coordinate)
                            {
                                Coordinate = Playerships[k].Name.Substring(0, 1);
                                isShip = true;
                                break;
                            }
                        }
                    }
                    if (!isShip)
                    {
                        Coordinate = "-";
                    }
                    Console.Write($"{Coordinate} ");
                    isShip = false;
                }
                Console.WriteLine();
            }
                
        }
        public static string GetPlayerName(int playerCount)
        {
            Console.Clear();
            Console.Write($"Hello Player{playerCount}, please type your name : ");
            return Console.ReadLine();
        }
        public static void ShowStartingGrid(string name)
        {
            Console.Clear();
            Console.WriteLine($"Hello {name}, let's place your ships!\r\n\r\n   A B C D E F G H I J\r\n 1 - - - - - - - - - -\r\n 2 - - - - - - - - - -\r\n 3 - - - - - - - - - -\r\n 4 - - - - - - - - - -\r\n 5 - - - - - - - - - -\r\n 6 - - - - - - - - - -\r\n 7 - - - - - - - - - -\r\n 8 - - - - - - - - - -\r\n 9 - - - - - - - - - -\r\n10 - - - - - - - - - -");

            Console.WriteLine("Coordinates should be from A-J (column) and 1-10 (row).\r\nYou will be prompted for the starting coordinate and the direction of placement.");
            Console.WriteLine();
            Console.WriteLine();
        }

        public static void DisplayShipPlacementErrorMessage()
        {
            Console.WriteLine("Invalid Placement, please place again.");
        }

        public static void ShowShipHasBeenPlaced(Ship ship)
        {
            Console.WriteLine($"You have placed your {ship.Name}\r\nPress any key to continue...");
            Console.ReadLine();
        }

        public static string GetValidMove(IPlayer oppPlayer, Ship[] oppShips, IPlayer ourSelf)
        {
            Console.Clear();
            int shotsLeft = 17;
            int shipsLeft = 5;
            for (int z = 0; z < oppShips.Length; z++) //Checks all 5 ships
            {
                for (int l = 0; l < oppShips[z].Size; l++) //Check all cords of each ship
                {
                    for(int r = 0; r < ourSelf.shotsTaken.Length; r++)
                    {
                        if (ourSelf.shotsTaken[r] == oppShips[z].ShipCoordinates[l])
                        {
                            shotsLeft--;
                            break;
                        }
                    }
                    
                }
                if (oppShips[z].isSunk)
                {
                    shipsLeft--;
                }
            }
            Console.WriteLine($"{oppPlayer.Name} has {shipsLeft} ships remaining with {shotsLeft} hits left.");
            Console.WriteLine($"{ourSelf.Name}'s turn");
            if (ourSelf.isHuman)
            {


                //Display Grid of hits/misses
                string[] avaliableColums = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O" };

                for (int i = 0; i < 10; i++)
                {
                    if (i == 0)
                    {
                        Console.Write($"   {avaliableColums[i]} ");
                    }
                    else
                    {
                        Console.Write($"{avaliableColums[i]} ");
                    }
                }
                Console.WriteLine();
                for (int i = 1; i < 11; i++)
                {
                    if (i == 10)
                    {
                        Console.Write($"{i} ");
                    }
                    else
                    {
                        Console.Write($" {i} ");
                    }

                    for (int j = 1; j < 11; j++)
                    {
                        string coordinate = $"{avaliableColums[j - 1]}{i}";
                        bool isShip = false;
                        for (int k = 0; k < oppShips.Length; k++)
                        {
                            for (int l = 0; l < oppShips[k].Size; l++)
                            {
                                if (oppShips[k].ShipCoordinates[l] == coordinate)
                                {
                                    //check if guy is in our shots taken ;)
                                    for (int m = 0; m < ourSelf.shotsTaken.Length; m++)
                                    {
                                        if (ourSelf.shotsTaken[m] == coordinate)
                                        {
                                            coordinate = "H";
                                            isShip = true;
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                        for (int m = 0; m < ourSelf.shotsTaken.Length; m++)
                        {
                            if (ourSelf.shotsTaken[m] == coordinate)
                            {
                                coordinate = "M";
                                isShip = true;
                            }
                        }
                        if (!isShip)
                        {
                            coordinate = "-";
                        }
                        Console.Write($"{coordinate} ");
                        isShip = false;
                    }
                    Console.WriteLine();
                }

                string[] validColums = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
                string Coordinate = "";
                do
                {
                    Console.WriteLine("Enter target coordinate (ex: A5): ");
                    Coordinate = Console.ReadLine();
                    if (string.IsNullOrEmpty(Coordinate))
                    {
                        Console.WriteLine("Please enter a valid Coordinate");
                        continue;
                    }
                    string column = Coordinate.Substring(0, 1);

                    if (!validColums.Contains(column))
                    {
                        Console.WriteLine("Please enter a valid Coordinate");
                        continue;
                    }
                    int row;
                    if (!int.TryParse(Coordinate.Substring(1), out row))
                    {
                        Console.WriteLine("Please enter a valid Coordinate");
                        continue;
                    }
                    if (row > 10 || row < 0)
                    {
                        Console.WriteLine("Please enter a valid Coordinate");
                        continue;
                    }
                    bool validHit = true;
                    for (int i = 0; i < ourSelf.shotsTaken.Length; i++)
                    {
                        if (ourSelf.shotsTaken[i] == Coordinate)
                        {
                            Console.WriteLine("You already tried shooting at that square, try a different one.");
                            validHit = false;
                            break;
                        }
                    }
                    if (validHit)
                    {
                        break;
                    }
                } while (true);
                return Coordinate;
            }
            return null;
        }

        public static void DisplayTurn(MoveResult moveResult, string name, string moveChoice)
        {

            Console.WriteLine($"{name} fires a shot at {moveChoice}");
            if (MoveResult.Hit == moveResult)
            {
                Console.Write("Boom! They ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("hit ");
                Console.ResetColor();
                Console.Write("something!");
                Console.WriteLine();
            }
            else if (MoveResult.Miss == moveResult)
            {
                Console.ForegroundColor= ConsoleColor.Blue;
                Console.Write("Splash! ");
                Console.ResetColor();
                Console.Write("The shot missed!");
                Console.WriteLine();
            }
            else if (MoveResult.Sunk == moveResult)
            {
                Console.WriteLine("Boom! Gurgle! The ship is sunk!");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }

        public static void DisplayWinMessage(string name)
        {
            Console.WriteLine($"Congrats {name} has won the game!");
        }

        public static bool IsHumanPlayer(string prompt)
        {
            string val;
            do
            {
                Console.WriteLine(prompt);
                val = Console.ReadLine();

                if(val == "H")
                {
                    return true;
                }else if(val == "C")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Please enter a valid selection");
                }
            } while (true);
        }
    }
}
