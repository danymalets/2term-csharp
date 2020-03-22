using System;
using System.Threading;

namespace game2048
{
    class Program
    {
        const int size = 4;

        static bool BOT;
        static int SLEEP;
        static int N;

        static void Main(string[] args)
        {
            Program program = new Program();
            int cnt = 0;

           
            Console.WriteLine("Enter 1 - BOT, 2 - YOU");
            string s = Console.ReadLine();
            if (s == "1")
            {
                BOT = true;
            }
            else if (s == "2")
            {
                BOT = false;
            }
            else{
                Console.WriteLine("Error");
                return;
            }
            if (BOT)
            {
                Console.WriteLine("Enter the number of starts");
                try
                {
                    N = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Error");
                    return;
                }


                Console.WriteLine("Enter SLEEP");
                try
                {
                    SLEEP = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Error");
                    return;
                }
            }
            else
            {
                N = 1;
            }
            for (int i = 0; i < N; i++)
            {
                if (program.Start()) cnt++;
            }
            Console.WriteLine(cnt + " / " + N);
        }

        bool Start()
        {
            Model model = new Model(size);
            Bot bot = new Bot(size);
            while (true)
            {
                model.Show();

                if (BOT && model.IsWin()) return true;
                if (model.IsGameOver())
                {
                    if (BOT) return false;
                    Console.WriteLine("Game Over");
                    return false;
                }
                if (BOT)
                {
                    Thread.Sleep(SLEEP / 2);


                    switch (bot.Ask(model))
                    {
                        case Bot.Dir.Left: Console.WriteLine("<"); model.Left(); break;
                        case Bot.Dir.Rigth: Console.WriteLine(">"); model.Rigth(); break;
                        case Bot.Dir.Up: Console.WriteLine("^"); model.Up(); break;
                        case Bot.Dir.Down: Console.WriteLine("V"); model.Down(); break;
                        default:
                            break;
                    }
                    Thread.Sleep(SLEEP / 2);
                }
                else
                {
                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.LeftArrow: model.Left(); break;
                        case ConsoleKey.RightArrow: model.Rigth(); break;
                        case ConsoleKey.UpArrow: model.Up(); break;
                        case ConsoleKey.DownArrow: model.Down(); break;
                        case ConsoleKey.G: Start(); break;
                    }
                }
               

            }
        }
    }
}
