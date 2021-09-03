using System;

namespace C_
{
    class Program
    {
        static void Main(string[] args)
        {
            const string play = "Où voulez vous jouer ?";
            int[,] board = {
                {0,0,0},
                {0,0,0},
                {0,0,0}
            };
            int[] numbers = { 0, 1, 2 };
            int turn = 2;
            int count = 0;
            bool win = display(board, turn, 3, 3);
            string position;
            do
            {
                turn = turn == 2 ? 1 : 2;
                int number_pos;
                string letter;
                int letter_pos = 3;
                bool loop;
                do
                {
                    Console.WriteLine(play + " (Joueur " + turn + ")");
                    position = Console.ReadLine();
                    number_pos = position.Length > 1 ? Int32.Parse(Convert.ToString(position[1])) - 1 : 3;
                    letter = position.Length > 0 ? Convert.ToString(position[0]).ToUpper() : "D";
                    letter_pos = letter == "A" ? 0 : letter == "B" ? 1 : letter == "C" ? 2 : 3;
                    Console.WriteLine(number_pos + " / " + letter_pos);
                    if (!in_array(numbers, number_pos) || !in_array(numbers, letter_pos))
                    {
                        Console.WriteLine("Case inexistante");
                        loop = true;
                    }
                    else if (board[letter_pos, number_pos] != 0)
                    {
                        Console.WriteLine("Case déjà occupé");
                        loop = true;
                    }
                    else
                    {
                        loop = false;
                    }
                } while (loop);
                board[letter_pos, number_pos] = turn;
                win = display(board, turn, letter_pos, number_pos);
                count++;
            } while (win == false && count < 9);
            if (win)
            {
                Console.WriteLine("Gagnant : Joueur " + turn);
            }
            else
            {
                Console.WriteLine("Partie nulle");
            }
        }

        static bool display(int[,] board, int turn, int row, int col)
        {
            Console.Clear();
            Console.WriteLine("   1 2 3 ");
            Console.WriteLine("  +-+-+-+");
            Console.WriteLine("A |" + board[0, 0] + "|" + board[0, 1] + "|" + board[0, 2] + "|");
            Console.WriteLine("  +-+-+-+");
            Console.WriteLine("B |" + board[1, 0] + "|" + board[1, 1] + "|" + board[1, 2] + "|");
            Console.WriteLine("  +-+-+-+");
            Console.WriteLine("C |" + board[2, 0] + "|" + board[2, 1] + "|" + board[2, 2] + "|");
            Console.WriteLine("  +-+-+-+");
            if (row != 3 && col != 3)
            {
                if (win(board, col, row, 1, 0, turn, 1, false))
                {
                    return true;
                }
                else if (win(board, col, row, 0, 1, turn, 1, false))
                {
                    return true;
                }
                else if (win(board, col, row, 1, 1, turn, 1, false))
                {
                    return true;
                }
                else if (win(board, col, row, 1, -1, turn, 1, false))
                {
                    return true;
                }
            }
            return false;
        }

        static bool win(int[,] board, int col, int row, int modif_col, int modif_row, int turn, int line, bool reverse)
        {
            if (col + modif_col < 3 && col + modif_col > -1 && row + modif_row < 3 && row + modif_row > -1 && board[row + modif_row, col + modif_col] == turn)
            {
                if (line == 1)
                {
                    return win(board, col, row, modif_col * 2, modif_row * 2, turn, 2, reverse);
                }
                else
                {
                    return true;
                }
            }
            else if (!reverse)
            {
                modif_col = modif_col != 0 ? modif_col / Math.Abs(modif_col) * -1 : 0;
                modif_row = modif_row != 0 ? modif_row / Math.Abs(modif_row) * -1 : 0;
                return win(board, col, row, modif_col, modif_row, turn, line, true);
            }
            else
            {
                return false;
            }
        }

        static bool in_array(char[] array, int position)
        {
            for (int i = 0; i < 3; i++)
            {
                if (array[i] == position)
                {
                    return true;
                }
            }
            return false;
        }

        static bool in_array(int[] array, int position)
        {
            for (int j = 0; j < 3; j++)
            {
                if (array[j] == position)
                {
                    return true;
                }
            }
            return false;
        }
    }
}