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
        private readonly int size = 3;
        private readonly int draw_cell_size = 3;

        public Game(int size = 3)
        {
            this.size = size;
        }

        private void Init_board()
        {
            /* create board with numbers from 1 to size*size
             * 
             * | 1 | 2 | 3 |
             * | 4 | 5 | 6 |
             * | 7 | 8 | 9 |
             * 
             */
            board = new string[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    board[i, j] = (i * size + j + 1).ToString();
                }
            }
        }

        private bool Is_winner(int cell)
        {
            bool row_win = true, col_win = true, diagonal_up_down_win = true, diagonal_down_up_win = true;
            int i, j, count = 0, row = cell / size, col = cell % size;
            /* check row and col win:
             * 
             * |   |   |   |
             * | x | x | x | -> row win
             * |   |   |   |
             * 
             * * * * * * * * * * * * *
             * 
             * |   | x |   |
             * |   | x |   | -> col win
             * |   | x |   |
             */
            for (i = 0; i < size; i++)
            {
                if (board[row, i] != board[row, col]) 
                    row_win = false;

                if (board[i, col] != board[row, col])
                    col_win = false;
            }

            /* check diagonal from up to down win:
             * 
             * | x |   |   |
             * |   | x |   | -> diagonal up down win
             * |   |   | x |
             * 
             */
            // find the must up left cell in the resive cell diagonal
            i = row - col > 0 ? row - col : 0;
            j = col - row > 0 ? col - row : 0;
            for (; i < size && j < size; i++, j++)
            {
                if (board[i, j] != board[row, col])
                {
                    diagonal_up_down_win = false;
                    break;
                }
                count++;
            }

            /* check diagonal from down to up win:
             * 
             * |   |   | x |
             * |   | x |   | -> diagonal down up win
             * | x |   |   |
             * 
             */
            // find the must down left cell in the resive cell diagonal
            count = 0;
            i = row + Math.Min(size - 1 - row, col);
            j = col - Math.Min(size - 1 - row, col);
            for (; i >= 0 && j < size; i--, j++)
            {
                if (board[i, j] != board[row, col])
                {
                    diagonal_down_up_win = false;
                    break;
                }
                count++;
            }

            // check if the diagonal lenght equals to the board size
            diagonal_up_down_win = diagonal_up_down_win && count - size == 0;
            diagonal_down_up_win = diagonal_down_up_win && count - size == 0;

            return row_win || col_win || diagonal_up_down_win || diagonal_down_up_win;
        }

        private int Move(int cell, string player)
        {
            // if choosen cell contian a number, replace it by the player sign
            if (int.TryParse(board[cell / size, cell % size], out _))
            {
                board[cell / size, cell % size] = player;
                // check for winning
                return Is_winner(cell) ? 1 : 0;
            }

            // already played cell
            else
                return -1;
        }

        public void Start_game()
        {
            Init_board();
            string[] player = { player1, player2 };
            int status;
            for (int turn = 0; turn < size*size; turn++)
            {
                while (true)
                {
                    // draw the board and wait to players move
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
            // stop the game after the winning
            string[] player = { player1, player2 };
            Draw();
            if (winner == -1)
                Console.WriteLine("Tie, no winners");

            else
                Console.WriteLine("Player {0} has won!", player[winner]);
        }

        private string draw_number(string number)
        {
            switch (number.Length)
            {
                case 1:
                    return "  " + number + "  ";
                case 2:
                    return "  " + number + " ";
                case 3:
                    return " " + number + " ";
                case 4:
                    return " " + number + "";
                case 5:
                    return number;
                default:
                    return "     ";
            }
        }

        private void Draw()
        {
            Console.Clear();
            string upper_line = " _____";
            for (int i = 1; i < size - 1; i++)
            {
                upper_line += " _____";
            }

            upper_line += " _____ ";
            Console.WriteLine(upper_line);
            for (int row = 0; row < size; row++)
            {
                for (int line = 0; line < draw_cell_size; line++)
                {
                    Console.Write("|");
                    for (int col = 0; col < size; col++)
                    {
                        if (line == 0)
                            Console.Write("     ");

                        else if (line == 1)
                            Console.Write(draw_number(board[row, col]));

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
