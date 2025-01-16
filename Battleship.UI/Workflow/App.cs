using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.UI.Factory;
using Battleship.UI.GameLogic;
using Battleship.UI.Implementations;
using Battleship.UI.InputManagement;
using Battleship.UI.Interfaces;

namespace Battleship.UI.Workflow
{
    public static class App
    {
        public static void Run()
        {
            Console.WriteLine("Welcome to battleship!!");
            IPlayer player1 = GameManagerFactory.isHuman("Is player 1 a (H)uman or (C)omputer?: ");
            IPlayer player2 = GameManagerFactory.isHuman("Is player 2 a (H)uman or (C)omputer?");
            GameManager gm = new GameManager(player1, player2);
            if (player1 is HumanPlayer)
            {
                player1.Name = ConsoleIO.GetPlayerName(1);
                player1.isHuman = true;
            }
            else
            {
                player1.isHuman = false;
                player1.Name = "CPU";
            }
            gm.IniliazeGame(player1);
            if (player2 is HumanPlayer)
            {
                player2.isHuman = true; 
                player2.Name = ConsoleIO.GetPlayerName(2);
            }
            else
            {
                player2.isHuman = false;
                player2.Name = "CPU";
            }
            gm.IniliazeGame(player2);
            do
            {
                //Game is now Iniliazed with both players having all of their ships placed, now we want to try to shoot people!
                ConsoleIO.DisplayTurn(gm.PlayRound(player1), player1.Name, player1.lastShot);
                if (gm.GameOver(player1))
                {
                    ConsoleIO.DisplayWinMessage(player1.Name);
                    break;
                }
                ConsoleIO.DisplayTurn(gm.PlayRound(player2), player2.Name, player2.lastShot);
                if (gm.GameOver(player2))
                {
                    ConsoleIO.DisplayWinMessage(player2.Name);
                    break;
                }

            } while (true);
        }
    }
}
