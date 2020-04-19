using System;

namespace TicTacToe
{
    class Program
    {
        static Game game;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Enter size (3 <= size <= 5):");
                int size;
                while (!int.TryParse(Console.ReadLine(), out size) || size < 3 || size > 5)
                {
                    Console.WriteLine("Try again");
                }
                Console.WriteLine("Enter:\n" +
                                  "1. If you want to play first (X)\n" +
                                  "2. If you want to play second (O)");
                int userMoveNumber;
                while (!int.TryParse(Console.ReadLine(), out userMoveNumber) || userMoveNumber < 1 || userMoveNumber > 2)
                {
                    Console.WriteLine("Try again");
                }
                Play(userMoveNumber, size);
                Console.WriteLine("Enter:\n" +
                                  "1. Play again\n" +
                                  "2. Exit");
                int action;
                while (!int.TryParse(Console.ReadLine(), out action) || action < 1 || action > 2)
                {
                    Console.WriteLine("Try again");
                }
                if (action == 2) return;
            }
            
        }

        static void Play(int userMoveNumber, int size)
        {
            game = new Game(size, userMoveNumber);
            if (userMoveNumber == 2)
            {
                game.BotMove();
            }
            game.DisplayField();
            while (true)
            {
                game.UserMove(GetUserMove());
                game.DisplayField();
                if (game.IsUserWin())
                {
                    Console.WriteLine("Win!");
                    return;
                }
                if (game.IsEnd())
                {
                    Console.WriteLine("Draw.");
                    return;
                }
                game.BotMove();
                game.DisplayField();
                if (game.IsBotWin())
                {
                    Console.WriteLine("Lose!");
                    return;
                }
                if (game.IsEnd())
                {
                    Console.WriteLine("Draw.");
                    return;
                }
            }
        }

        static int GetUserMove()
        {
            Console.WriteLine("Enter number");
            int move;
            while (!int.TryParse(Console.ReadLine(), out move) || !game.IsValidMove(move - 1)){
                Console.WriteLine("Try again");
            }
            return move - 1;
        }
    }
}
