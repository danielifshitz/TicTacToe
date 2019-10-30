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
            Console.WriteLine("please enter board size");
            string choose = Console.ReadLine();
            int result;
            while (! int.TryParse(choose, out result))
            {
                Console.WriteLine("please enter board size");
                choose = Console.ReadLine();
            }
            Game board = new Game(result);
            while (true)
            {
                board.Start_game();
                Console.WriteLine("Press any key to reset the game");
                Console.ReadKey();
            }
        }
    }
}
