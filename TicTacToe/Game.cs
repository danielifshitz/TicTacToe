using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Game
    {
        private string[,] board;
        private readonly string player1 = "X";
        private readonly string player2 = "O";

        private void Init_board()
        {
            board = new string[3, 3]
            {
                {"1", "2", "3"},
                {"4", "5", "6"},
                {"7", "8", "9"}
            };
        }

        private string Board(int cell)
        {
            return board[cell / 3, cell % 3];
        }

        private bool Is_winner(int cell)
        {
            if ((cell == 0 || cell == 1 || cell == 2) && (Board(0) == Board(1) && Board(0) == Board(2)) ||
                (cell == 0 || cell == 3 || cell == 6) && (Board(0) == Board(3) && Board(0) == Board(6)) ||
                (cell == 0 || cell == 4 || cell == 8) && (Board(0) == Board(4) && Board(0) == Board(8)) ||
                (cell == 1 || cell == 4 || cell == 7) && (Board(1) == Board(4) && Board(1) == Board(7)) ||
                (cell == 2 || cell == 5 || cell == 8) && (Board(2) == Board(5) && Board(2) == Board(8)) ||
                (cell == 2 || cell == 4 || cell == 6) && (Board(2) == Board(4) && Board(2) == Board(6)) ||
                (cell == 3 || cell == 4 || cell == 5) && (Board(3) == Board(4) && Board(3) == Board(5)) ||
                (cell == 6 || cell == 7 || cell == 8) && (Board(6) == Board(7) && Board(6) == Board(8)))
                return true;

            return false;
        }

        private int Move(int cell, string player)
        {
            if (int.TryParse(Board(cell), out _))
            {
                board[cell / 3, cell % 3] = player;
                return Is_winner(cell) ? 1 : 0;
            }

            else
                return -1;
        }

        public void Start_game()
        {
            Init_board();
            string[] player = { player1, player2 };
            int status;
            for (int turn = 0; turn < 9; turn++)
            {
                while (true)
                {
                    Draw();
                    Console.WriteLine("Player {0}: Choose your field", player[turn % 2]);
                    string choose = Console.ReadLine();
                    if (int.TryParse(choose, out int result))
                    {
                        status = Move(result - 1, player[turn % 2]);
                        if (status == 0)
                            break;

                        else if (status == 1)
                        {
                            Winner(turn % 2);
                            return;
                        }
                    }

                    Console.WriteLine("Incorrect input, Please select another field!");
                    Console.ReadKey();
                }
            }

            Winner(-1);
        }

        private void Winner(int winner)
        {
            string[] player = { player1, player2 };
            Draw();
            if (winner == -1)
                Console.WriteLine("Tie, no winners");

            else
                Console.WriteLine("Player {0} has won!", player[winner]);
        }

        private void Draw()
        {
            Console.Clear();
            Console.WriteLine(" _________________ ");
            for (int row = 0; row < 3; row++)
            {
                for (int line = 0; line < 3; line++)
                {
                    Console.Write("|");
                    for (int col = 0; col < 3; col++)
                    {
                        if (line == 0)
                            Console.Write("     ");

                        else if (line == 1)
                            Console.Write("  {0}  ", board[row, col]);

                        else
                            Console.Write("_____");

                        Console.Write("|");
                    }

                    Console.WriteLine();
                }
            }

            Console.WriteLine();
        }
    }
}
