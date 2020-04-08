using System;

namespace maxpow2
{
    class Program
    {
        static ulong Solve(ulong n)
        {
            ulong sum = 0;
            ulong d = 2;
            for (int i = 1; i < 64; i++)
            {
                sum += n / d;
                d *= 2;
            }
            return sum;
        }

        static void Main(string[] args)
        {
            
            ulong l, r;
            Console.WriteLine("Enter l and r in different lines:");
            while (true)
            {
                try
                {
                    l = UInt64.Parse(Console.ReadLine());
                    if (l == 0)
                    {
                        Console.WriteLine("Error! Try again:");
                        continue;
                    }
                    r = UInt64.Parse(Console.ReadLine());
                    if (r == 0 || l > r)
                    {
                        Console.WriteLine("Error! Try again:");
                        continue;
                    }
                    break;
                }
                catch
                {
                    Console.WriteLine("Error! Try again:");
                }
            }
            Console.WriteLine("Max power of two = " + (Solve(r) - Solve(l - 1)));
        }
    }
}
