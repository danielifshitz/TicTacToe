using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            Game board = new Game();
            while (true)
            {
                board.Start_game();
                Console.WriteLine("Press any key to reset the game");
                Console.ReadKey();
            }
        }
    }
}
