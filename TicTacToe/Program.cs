using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;

namespace TicTacToe
{
    class Program
    {
        static int size = 8;
        static int rule = 4;
        static int currentTurn = 0;
        static int maxTurns = (int)Math.Pow(size, 2);
        static string[,] playField = setPlayField(size);
        static void Main(string[] args)
        {
            int player = 2;
            int input = 0;
            bool inputCorrect = true;
            List<int> slotOccupied = new List<int>();

            Introduction();

            do
            {
                if (player == 2)
                {
                    player = 1;
                }
                else if (player == 1)
                {
                    player = 2;
                }

                SetField(size);

                do
                {
                    Console.WriteLine($"\nPlayer {player}: Choose your field! ");
                    bool success = Int32.TryParse(Console.ReadLine(), out input);
                    if (!success || maxTurns < input || input <= 0)
                    {
                        Console.WriteLine($"Incorrect input, the input must be a number and in range (1-{maxTurns})");
                        inputCorrect = false;
                    }
                    else if (searchElement(input, slotOccupied))
                    {
                        Console.WriteLine("Incorrect input, the input has already occupied");
                        inputCorrect = false;
                    }
                    else
                    {
                        slotOccupied.Add(input);
                        inputCorrect = true;
                    }

                } while (!inputCorrect);

                EnterXorO(player, input);

                SetField(size);
            } while (true);
        }

        private static void SetField(int size)
        {
            Console.Clear();
            string line = "";
            for (int row = 0; row < size; row++)
            {
                string space = "      |";
                for (int col = 0; col < size; col++)
                {
                    line += space;
                }
                line += "\n";

                for (int col = 0; col < size; col++)
                {
                    if (playField[row, col] == "X")
                    {
                        line += $"  {playField[row, col]}   |";
                    }
                    else if (playField[row, col] == "O")
                    {
                        line += $"  {playField[row, col]}   |";
                    }
                    else
                    {
                        if (int.Parse(playField[row, col]) < 10)
                            line += $"  {playField[row, col]}   |";
                        else
                            line += $"  {playField[row, col]}  |";
                    }
                }
                line += "\n";
                string border = "______|";
                for (int col = 0; col < size; col++)
                {
                    line += border;
                }
                line += "\n";
            }
            Console.WriteLine(line);
        }

        public static string[,] setPlayField(int size)
        {
            string[,] playField = new string[size, size];
            int count = 1;
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    playField[row, col] = count.ToString();
                    count++;
                }
            }
            return playField;
        }


        public static void EnterXorO(int player, int input)
        {
            string playerSign = " ";
            if (player == 1)
                playerSign = "X";
            else if (player == 2)
                playerSign = "O";

            int row = input / size;
            int col = input % size;

            if (col == 0)
            {
                playField[row - 1, size - 1] = playerSign;
            }
            else
            {
                playField[row, col - 1] = playerSign;
            }

            currentTurn++;

            if (currentTurn >= 5 & isWinner(playerSign))
            {
                Console.WriteLine($"Congratulation! Player {player} is the winner");
                WinArt();
                System.Environment.Exit(0);
            }

            if (currentTurn == maxTurns)
            {
                Console.WriteLine("Draw");
                System.Environment.Exit(0);
            }
        }

        private static Boolean searchElement(int input, List<int> lst)
        {
            foreach (var item in lst)
            {
                if (item == input)
                    return true;
            }
            return false;
        }

        private static Boolean isWinner(string playerSign)
        {
            int count;

            // Check by row
            for (int row = 0; row < size; row++)
            {
                count = 0;
                for (int col = 0; col < size; col++)
                {
                    if (playField[row, col] == playerSign)
                    {
                        count++;
                        if (count == rule) return true;
                    }
                    else
                    {
                        count = 0;
                        continue;
                    }
                }
            }

            //Check by collumn
            for (int row = 0; row < size; row++)
            {
                count = 0;
                for (int col = 0; col < size; col++)
                {
                    if (playField[col, row] == playerSign)
                    {
                        count++;
                        if (count == rule) return true;
                    }
                    else
                    {
                        count = 0;
                        continue;
                    }
                }
            }

            // Check by diagonal
            for (int row = 0; row < rule; row++)
            {
                for (int col = 0; col < rule; col++)
                {
                    int i = row, j = col;
                    count = 0;
                    do
                    {
                        if (playField[i, j] == playerSign)
                        {
                            count++;
                            ++i;
                            ++j;
                            if (count == rule) return true;
                        }
                        else
                        {
                            count = 0;
                            ++i;
                            ++j;
                            continue;
                        }
                    } while (i < size & j < size);
                }
            }

            // Check by antidiagonal
            for (int row = 0; row <= size - rule; row++)
            {
                for (int col = rule - 1; col < size; col++)
                {
                    int i = row, j = col;
                    count = 0;
                    do
                    {
                        if (playField[i, j] == playerSign)
                        {
                            count++;
                            ++i;
                            --j;
                            if (count == rule) return true;
                        }
                        else
                        {
                            count = 0;
                            ++i;
                            --j;
                            continue;
                        }
                    } while (i < size & j >= 0);
                }
            }

            return false;
        }

        public static void Introduction()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(".-----. _         .-----.             .-----.            ");
            Console.WriteLine("`-. .-':_;        `-. .-'             `-. .-'            ");
            Console.WriteLine("  : :  .-. .--.     : : .--.   .--.     : : .--.  .--.   ");
            Console.WriteLine("  : :  : :'  ..'    : :' .; ; '  ..'    : :' .; :' '_.'  ");
            Console.WriteLine("  :_;  :_;`.__.'    :_;`.__,_;`.__.'    :_;`.__.'`.__.'  ");
            Console.ResetColor();
            Console.WriteLine("\nWelcome to Tic Tac Toe, please press any to begin");
            Console.ReadKey();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("RULES");
            Console.ResetColor();
            Console.WriteLine("Tic Tac Toe is a two player turn based game." +
                              "\nIt's you vs your opponent..." +
                              "\nPress any key to continue");
            Console.ReadKey();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("RULES");
            Console.ResetColor();
            Console.WriteLine("Players are represented with a unique signature" +
                              "\nPlayer 1 = X.  Player 2 = O");
            Console.WriteLine("\nThe first player to score three signatures in a row is the winner" +
                              "\nGood luck players! \nNow press any key to begin...");
            Console.ReadKey();
        }

        public static void WinArt()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" ÛÛÛÛÛ ÛÛÛÛÛ                        ÛÛÛÛÛ   ÛÛÛ   ÛÛÛÛÛ                     ÛÛÛ ÛÛÛ     ");
            Console.WriteLine("°°ÛÛÛ °°ÛÛÛ                        °°ÛÛÛ   °ÛÛÛ  °°ÛÛÛ                     °ÛÛÛ°ÛÛÛ     ");
            Console.WriteLine(" °°ÛÛÛ ÛÛÛ    ÛÛÛÛÛÛ  ÛÛÛÛÛ ÛÛÛÛ    °ÛÛÛ   °ÛÛÛ   °ÛÛÛ   ÛÛÛÛÛÛ  ÛÛÛÛÛÛÛÛ  °ÛÛÛ°ÛÛÛ     ");
            Console.WriteLine("  °°ÛÛÛÛÛ    ÛÛÛ°°ÛÛÛ°°ÛÛÛ °ÛÛÛ     °ÛÛÛ   °ÛÛÛ   °ÛÛÛ  ÛÛÛ°°ÛÛÛ°°ÛÛÛ°°ÛÛÛ °ÛÛÛ°ÛÛÛ     ");
            Console.WriteLine("   °°ÛÛÛ    °ÛÛÛ °ÛÛÛ °ÛÛÛ °ÛÛÛ     °°ÛÛÛ  ÛÛÛÛÛ  ÛÛÛ  °ÛÛÛ °ÛÛÛ °ÛÛÛ °ÛÛÛ °ÛÛÛ°ÛÛÛ     ");
            Console.WriteLine("    °ÛÛÛ    °ÛÛÛ °ÛÛÛ °ÛÛÛ °ÛÛÛ      °°°ÛÛÛÛÛ°ÛÛÛÛÛ°   °ÛÛÛ °ÛÛÛ °ÛÛÛ °ÛÛÛ °°° °°°      ");
            Console.WriteLine("    ÛÛÛÛÛ   °°ÛÛÛÛÛÛ  °°ÛÛÛÛÛÛÛÛ       °°ÛÛÛ °°ÛÛÛ     °°ÛÛÛÛÛÛ  ÛÛÛÛ ÛÛÛÛÛ ÛÛÛ ÛÛÛ     ");
            Console.WriteLine("    °°°°°     °°°°°°    °°°°°°°°         °°°   °°°       °°°°°°  °°°° °°°°° °°° °°°     ");
            Console.ResetColor();
        } //ASCII Art setup in it's own method to help keep the code tidy.
    }
}
