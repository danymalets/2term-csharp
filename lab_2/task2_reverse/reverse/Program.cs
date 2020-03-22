using System;

namespace reverse
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter string:");
            string s = Console.ReadLine();
            string[] arr = s.Split(" ");
            
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                if (arr[i] != "") Console.Write(arr[i] + " ");
            }
        }
    }
}
