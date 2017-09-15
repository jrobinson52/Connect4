using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    class Program
    {
        static char[,] spots = new char[6, 7];
        static bool full = false;
        static bool win = false;
        static bool player1 = true;


        static void Main(string[] args)
        {
            #region initial spots            //arrays write like this  x1 x2 x3
            for (int y = 0; y < 6; y++)      //                      y1
                for (int x = 0; x < 7; x++)  //                      y2
                    spots[y, x] = '-';       //                      y3
            #endregion



            while (!full && !win)
            {
                drawBoard();


                string v = "";

                while (v != "1" && v != "2" && v != "3" && v != "4" && v != "5" && v != "6" && v != "7")
                    try
                    {
                        v = Console.ReadLine()[0].ToString();
                    }
                    catch
                    {
                        drawBoard();
                    }

                int slot = Convert.ToInt32(v) - 1; //drops 1 to 0

                Console.WriteLine("Input a number from 1 to 7.");


                for (int y = 5; y >= 0; y--)
                    if (spots[y, slot] == '-')
                    {
                        spots[y, slot] = (player1) ? '*' : '0';
                        y = -1; //breaks loop
                        player1 = !player1;

                    }

                full = isFull();
                win = Gamewin();

            }
            drawBoard();


            if (Gamewin())
                Console.WriteLine(((player1) ? "Player 1" : "Player 2") + " wins!");

            if (full)
                Console.WriteLine("You filled the board without a winner.\nTIE!");



        }

        private static bool Gamewin()
        {


            int count = 0;
            char current = '0';

            #region horizontal
            for (int y = 0; y < 6; y++)
            {
                for (int x = 0; x < 7; x++)
                    if (spots[y, x] != '-')
                        if (spots[y, x] == current)
                        {
                            count++;
                            if (count == 4)
                            {
                                player1 = current == '0';
                                return true;
                            }
                        }
                        else
                        {
                            count = 1;
                            current = spots[y, x];
                        }
                count = 0;

            }
            #endregion

            #region vertical
            for (int x = 0; x < 7; x++)
            {
                for (int y = 0; y < 6; y++)
                    if (spots[y, x] != '-')
                        if (spots[y, x] == current)
                        {
                            count++;
                            if (count == 4)
                            {
                                player1 = current == '0';
                                return true;
                            }
                        }
                        else
                        {
                            count = 1;
                            current = spots[y, x];
                        }
                count = 0;

            }
            #endregion

            #region diagonal

            for (int y = 0; y < 6; y++)
            {
                for (int x = 0; x < 7; x++)
                    if (checkDiag(x, y) != '-')
                    {
                        player1 = spots[y, x] == '0';
                        return true;
                    }

            }
            #endregion

            return false;
        }

        private static void drawBoard()
        {
            Console.Clear();
            // display board
            Console.WriteLine("Enter the number for the column you wish to drop your token.");
            Console.WriteLine(((player1) ? "Player 1 (*)" : "Player 2 (0)") + "'s turn.");
            Console.WriteLine(" ");
            Console.WriteLine("1 2 3 4 5 6 7");

            for (int y = 0; y < 6; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    Console.Write(spots[y, x]);
                    if (x < 6)
                        Console.Write('|');
                }


                Console.WriteLine();

            }

            Console.WriteLine(" ");
            Console.WriteLine("Column:");


        }

        private static bool isFull()
        {
            for (int y = 0; y < 6; y++)
                for (int x = 0; x < 7; x++)
                    if (spots[y, x] == '-')
                        return false;

            return true;
        }

        private static char checkDiag(int x, int y)
        {
            char[] down = new char[] { cap(y - 3, x - 3), cap(y - 2, x - 2), cap(y - 1, x - 1), cap(y, x), cap(y + 1, x + 1), cap(y + 2, x + 2), cap(y + 3, x + 3) };
            char[] up = new char[] { cap(y - 3, x + 3), cap(y - 2, x + 2), cap(y - 1, x + 1), cap(y, x), cap(y + 1, x - 1), cap(y + 2, x - 2), cap(y + 3, x - 3) };

            int count = 0;
            for (int i = 0; i < down.Length; i++)
                if (down[i] == spots[y, x])
                {
                    count++;
                    if (count > 3)
                        return spots[y, x];
                }
                else count = 0;

            for (int i = 0; i < up.Length; i++)
                if (up[i] == spots[y, x])
                {
                    count++;
                    if (count > 3)
                        return spots[y, x];
                }
                else count = 0;


            return '-';
        }

        public static char cap(int x, int y)
        {
            if (x < 0 || x > 6 || y < 0 || y > 5)
                return '-';
            return spots[y, x];
        }


    }
}
