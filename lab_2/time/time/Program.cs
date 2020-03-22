using System;
using System.Globalization;

namespace time
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime date = DateTime.Now;
            string s;
            int[] cnt = new int[10];

            s = date.ToString("G", CultureInfo.CreateSpecificCulture("es-ES"));
            Console.WriteLine(s);
            Console.WriteLine();
            for (int i = 0; i < 10; i++)
                cnt[i] = 0;
            for (int i = 0; i < s.Length; i++)
                if (char.IsDigit(s[i]))
                    cnt[s[i] - '0']++;
            for (int i = 0; i < 10; i++)
                Console.WriteLine(i + " - " + cnt[i]);

            Console.WriteLine();
            Console.WriteLine();

            s = date.ToString("D", CultureInfo.CreateSpecificCulture("en-US"));
            Console.WriteLine(s);
            Console.WriteLine();
            for (int i = 0; i < 10; i++)
                cnt[i] = 0;
            for (int i = 0; i < s.Length; i++)
                if (char.IsDigit(s[i]))
                    cnt[s[i] - '0']++;
            for (int i = 0; i < 10; i++)
                Console.WriteLine(i + " - " + cnt[i]);
        }
    }
}
