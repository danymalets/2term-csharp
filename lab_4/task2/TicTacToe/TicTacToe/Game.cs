using System;
using System.Runtime.InteropServices;

namespace TicTacToe
{
    class Game
    {
        [DllImport("TicTacToeBotDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Init(int size);

        [DllImport("TicTacToeBotDLL.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void MakeMove(int move);

        [DllImport("TicTacToeBotDLL.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int GetMove();

        int Size;
        char UserSymbol, BotSymbol;
        char[,] Field;

        public Game (int size, int userMoveNumber)
        {
            Size = size;
            Field = new char[Size, Size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    Field[i, j] = ' ';
            if (userMoveNumber == 1)
            {
                UserSymbol = 'X';
                BotSymbol = 'O';
            }
            else
            {
                UserSymbol = 'O';
                BotSymbol = 'X';
            }
            Init(size);
        }

        public bool IsValidMove(int a)
        {
            return a >= 0 && a < Size * Size && Field[a / Size, a % Size] == ' '; 
        }

        public void UserMove(int a)
        {
            MakeMove(a);
            int i = a / Size;
            int j = a % Size;
            Field[i, j] = UserSymbol;
        }

        public void BotMove()
        {
            int a = GetMove();
            int i = a / Size;
            int j = a % Size;
            Field[i, j] = BotSymbol;
        }

        public bool IsEnd()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (Field[i, j] == ' ') return false;
                }
            }
            return true;
        }

        public bool IsUserWin()
        {
            return IsWin(UserSymbol);
        }

        public bool IsBotWin()
        {
            return IsWin(BotSymbol);
        }

        bool IsWin(char symbol)
        {
            bool ok;
            for (int i = 0; i < Size; i++)
            {
                ok = true;
                for (int j = 0; j < Size; j++)
                {
                    if (Field[i, j] != symbol) ok = false;
                }
                if (ok) return true;
            }
            for (int j = 0; j < Size; j++)
            {
                ok = true;
                for (int i = 0; i < Size; i++)
                {
                    if (Field[i, j] != symbol) ok = false;
                }
                if (ok) return true;
            }
            ok = true;
            for (int i = 0, j = 0; i < Size; i++, j++)
            {
                if (Field[i, j] != symbol) ok = false;
            }
            if (ok) return true;
            ok = true;
            for (int i = 0, j = Size - 1; i < Size; i++, j--)
            {
                if (Field[i, j] != symbol) ok = false;
            }
            if (ok) return true;
            return false;
        }


        public void DisplayField()
        {
            Console.Clear();
            Console.WriteLine("You - " + UserSymbol);
            Console.WriteLine("Bot - " + BotSymbol);
            for (int i = 0; i < Field.GetLength(0); i++)
            {
                if (i != 0)
                {
                    for (int j = 0; j < Field.GetLength(1); j++)
                    {
                        if (j != 0) Console.Write("┼");
                        Console.Write("─────");
                    }
                    Console.Write("          ");
                    for (int j = 0; j < Field.GetLength(1); j++)
                    {
                        if (j != 0) Console.Write("┼");
                        Console.Write("─────");
                    }
                }
                Console.WriteLine();
                for (int j = 0; j < Field.GetLength(1); j++)
                {
                    if (j != 0) Console.Write("│");
                    Console.Write("     ");
                }
                Console.Write("          ");
                for (int j = 0; j < Field.GetLength(1); j++)
                {
                    if (j != 0) Console.Write("│");
                    Console.Write("     ");
                }
                Console.WriteLine();
                for (int j = 0; j < Field.GetLength(1); j++)
                {
                    if (j != 0) Console.Write("│");
                    Console.Write("  " + Field[i, j] + "  ");
                }
                Console.Write("          ");
                for (int j = 0; j < Field.GetLength(1); j++)
                {
                    if (j != 0) Console.Write("│");
                    int num = i * Field.GetLength(0) + j + 1;
                    if (num < 10) Console.Write("  " + num + "  ");
                    else Console.Write("  " + num + " ");
                }
                Console.WriteLine();
                for (int j = 0; j < Field.GetLength(1); j++)
                {
                    if (j != 0) Console.Write("│");
                    Console.Write("     ");
                }
                Console.Write("          ");
                for (int j = 0; j < Field.GetLength(1); j++)
                {
                    if (j != 0) Console.Write("│");
                    Console.Write("     ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
