using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.UI.Implementations;
using Battleship.UI.InputManagement;
using Battleship.UI.Interfaces;

namespace Battleship.UI.Factory
{
    public static class GameManagerFactory
    {
        

        public static IPlayer isHuman(string prompt)
        {
            if (ConsoleIO.IsHumanPlayer(prompt))
            {
                return new HumanPlayer();
            }
            else
            {
                return new ComputerPlayer();
            }
        }
    }
}
