using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Battleship.UI.InputManagement;
using Battleship.UI.Interfaces;

namespace Battleship.UI.GameLogic
{
    public class GameManager
    {
        IPlayer Player1 { get; set; }
        IPlayer Player2 { get; set; }
        public GameManager(IPlayer player1, IPlayer player2)
        {
            Player1 = player1;
            Player2 = player2;
        }

        public void IniliazeGame(IPlayer currentPlayer)
        {
            string[] shipPlacement;
            if (currentPlayer.isHuman)
            {
                ConsoleIO.ShowStartingGrid(currentPlayer.Name);
            }
            
            for (int i = 0; i < 5; i++)
            {
                do
                {
                   shipPlacement = currentPlayer.PlaceShips(i);
                   if(ValidateShipPlacement(shipPlacement, currentPlayer.PlayerShips))
                    {
                        break;
                    }
                    else
                    {
                        if(currentPlayer.isHuman == true)
                        {
                            ConsoleIO.DisplayShipPlacementErrorMessage();
                        }   
                    }
                } while (true);
                currentPlayer.PlayerShips[i].ShipCoordinates = shipPlacement;
                if (currentPlayer.isHuman == true)
                {
                    ConsoleIO.ShowShipHasBeenPlaced(currentPlayer.PlayerShips[i]);
                    ConsoleIO.DisplayGrid(currentPlayer.PlayerShips);
                }
            }
        }

        public MoveResult PlayRound(IPlayer currentPlayer)
        {
            Ship[] targetShips;
            String moveChoice;
            if (Player1 == currentPlayer)
            {
                targetShips = Player2.PlayerShips;
                moveChoice = currentPlayer.getMove(Player2, Player2.PlayerShips);
            }
            else
            {
                targetShips = Player1.PlayerShips;
                moveChoice = currentPlayer.getMove(Player1, Player1.PlayerShips);
            }
            
            currentPlayer.lastShot = moveChoice;
            currentPlayer.shotsTaken[currentPlayer.turnNumber] = moveChoice;

            currentPlayer.turnNumber++;
            for (int k = 0; k < targetShips.Length; k++)
            {
                for (int l = 0; l < targetShips[k].Size; l++)
                {
                    if (targetShips[k].ShipCoordinates[l] == moveChoice)
                    {
                        int checkSunk = 0;
                        for (int m = 0; m < targetShips[k].ShipCoordinates.Length; m++)
                        {
                            for (int n = 0; n < currentPlayer.shotsTaken.Length; n++)
                            {
                                if (currentPlayer.shotsTaken[n] == targetShips[k].ShipCoordinates[m])
                                {

                                    checkSunk++;
                                    
                                }
                            }
                            
                        }
                        if(checkSunk == targetShips[k].ShipCoordinates.Length)
                        {
                            targetShips[k].isSunk = true;
                            return MoveResult.Sunk;
                        }
                        else
                        {
                            return MoveResult.Hit;
                        }
                        
                    }
                }
            }
            return MoveResult.Miss;
        }
        public bool GameOver(IPlayer currentPlayer)
        {
            Ship[] targetShips;
            if (Player1 == currentPlayer)
            {
                targetShips = Player2.PlayerShips;
            }
            else
            {
                targetShips = Player1.PlayerShips;
            }
            int sunkCounter = 0;
            for (int k = 0; k < targetShips.Length; k++)
            {
                if (targetShips[k].isSunk)
                {
                    sunkCounter++;
                }
            }
            if (sunkCounter == 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidateShipPlacement(String[] projectedPlacement, Ship[] PlayerShips)
        {
            string[] validColumns = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            bool validSelection = true;
            for (int i = 0; i < projectedPlacement.Length; i++)
            {
                string letter = projectedPlacement[i].Substring(0, 1);
                if (!validColumns.Contains(letter))
                {
                    validSelection = false;
                    continue;
                    //Give Error to ConsoleIO or some shit
                }
                int number = int.Parse(projectedPlacement[i].Substring(1));
                if (number > 10 || number < 0)
                {
                    validSelection = false;
                    continue;
                    //Give error to ConsoleIO or some shit
                }
            }
            //Now we need to verify that it doesn't overlap or go off grid

            for (int k = 0; k < PlayerShips.Length; k++)
            {
                for (int l = 0; l < PlayerShips[k].Size; l++)
                {
                    for (int i = 0; i < projectedPlacement.Length; i++)
                    {
                        if (PlayerShips[k].ShipCoordinates[l] == projectedPlacement[i])
                        {
                            validSelection = false;
                        }
                    }
                }
            }
            return validSelection;
        }
    }
}
