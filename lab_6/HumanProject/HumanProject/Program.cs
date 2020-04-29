using System;
using System.Collections.Generic;
using System.Globalization;

namespace HumanProject
{
    class Program
    {
        static List<Runner> runners;

        static List<Boxer> boxers;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter:\n" +
                                  "1. Open runners menu\n" +
                                  "2. Open boxers menu\n" +
                                  "3. Exit");
                int action = ReadInt(3);
                if (action == 1)
                {
                    runners = new List<Runner>()
                    {
                        new Runner("Usain Bolt", "0", 37),
                        new Runner("Tyson Gay", "1", 36),
                        new Runner("Yohan Blake", "2", 35),
                        new Runner("Richard Tompson","3", 33),
                        new Runner("Petya","4", 10)
                    };
                    while (true)
                    {
                        Console.WriteLine("List:");
                        for (int i = 0; i < runners.Count; i++)
                        {
                            Console.WriteLine(runners[i]);
                        }
                        Console.WriteLine("Enter:\n" +
                                          "1. Add\n" +
                                          "2. Remove\n" +
                                          "3. Run a race\n" +
                                          "4. Exit");
                        action = ReadInt(4);
                        if (action == 1)
                        {
                            Runner runner = ReadRunner();
                            if (runner == null) continue;
                            runners.Add(runner);
                        }
                        else if (action == 2)
                        {
                            Console.WriteLine("Enter identifier");
                            string id = Console.ReadLine();
                            int ind = FindRunner(id);
                            if (ind != -1) runners.RemoveAt(ind);
                        }
                        else if (action == 3)
                        {
                            Console.WriteLine("Enter metres");
                            int metres = ReadInt(1000 * 100);
                            List<(int, string, string)> list = new List<(int, string, string)>();
                            foreach (var runner in runners)
                            {
                                list.Add((runner.Run(metres), runner.FullName, runner.Identifier));
                            }
                            list.Sort();
                            for (int i = 0; i < list.Count; i++)
                            {
                                Console.WriteLine(string.Format("{0}. {1} (id={2}) --- result = {3} ms",
                                    i + 1, list[i].Item2, list[i].Item3, list[i].Item1));
                            }
                            if (list.Count != 0) Console.WriteLine("WINNER - " + list[0].Item2);
                        }
                        else break;
                    }
                }
                else if (action == 2)
                {
                    boxers = new List<Boxer>()
                    {
                        new Boxer("Floyd Mayweather", "0", 12000, 15000),
                        new Boxer("Manny Pacquiao", "1", 1100, 1100),
                        new Boxer("Archie Moore", "2", 790, 820),
                        new Boxer("Ray Leonard", "3", 750, 750),
                        new Boxer("Larry Holmes", "4", 300, 180)
                    };
                    while (true)
                    {
                        Console.WriteLine("List:");
                        for (int i = 0; i < boxers.Count; i++)
                        {
                            Console.WriteLine(boxers[i]);
                        }
                        Console.WriteLine("Enter:\n" +
                                          "1. Add\n" +
                                          "2. Remove\n" +
                                          "3. Organize a tournament\n" +
                                          "4. Exit");
                        action = ReadInt(4);
                        if (action == 1)
                        {
                            Boxer boxer = ReadBoxer();
                            if (boxer == null) continue;
                            boxers.Add(boxer);
                        }
                        else if (action == 2)
                        {
                            Console.WriteLine("Enter identifier");
                            string id = Console.ReadLine();
                            int ind = FindBoxer(id);
                            if (ind != -1) boxers.RemoveAt(ind);
                        }
                        else if (action == 3)
                        {
                            List<(int, string, string)> list = new List<(int, string, string)>();
                            foreach (var boxer in boxers)
                            {
                                list.Add((0, boxer.FullName, boxer.Identifier));
                            }
                            for (int i = 0; i < boxers.Count - 1; i++)
                            {
                                for (int j = i + 1; j < boxers.Count; j++)
                                {
                                    if (boxers[i].FightWith(boxers[j]))
                                        list[i] = (list[i].Item1 + 1, list[i].Item2, list[i].Item3);
                                    else
                                        list[j] = (list[j].Item1 + 1, list[j].Item2, list[j].Item3);
                                }
                            }
                            list.Sort();
                            list.Reverse();
                            for (int i = 0; i < list.Count; i++)
                            {
                                Console.WriteLine(string.Format("{0}. {1} (id={2}) --- result = {3} wins",
                                    i + 1, list[i].Item2, list[i].Item3, list[i].Item1));
                            }
                            if (list.Count != 0) Console.WriteLine("WINNER - " + list[0].Item2);
                        }
                        else break;
                    }
                }
                else break;
            }
        }

        static int FindRunner(string id)
        {
            for (int i = 0; i < runners.Count; i++)
            {
                if (runners[i].Identifier == id) return i;
            }
            return -1;
        }

        static Runner ReadRunner()
        {
            Console.WriteLine("Enter full name");
            string fullName = Console.ReadLine();
            if (fullName.Length == 0)
            {
                Console.WriteLine("The name must not be empty");
                return null;
            }
            Console.WriteLine("Enter identifier");
            string identifier = Console.ReadLine();
            if (fullName.Length == 0)
            {
                Console.WriteLine("The identifier must not be empty");
                return null;
            }
            else if (FindRunner(identifier) != -1)
            {
                Console.WriteLine("The identifier must be individual");
                return null;
            }
            Console.WriteLine("Enter speed");
            if (!double.TryParse(Console.ReadLine(), out double speed) || speed < 0)
            {
                Console.WriteLine("Error");
                return null;
            }
            return new Runner(fullName, identifier, speed);
        }

        static int FindBoxer(string id)
        {
            for (int i = 0; i < boxers.Count; i++)
            {
                if (boxers[i].Identifier == id) return i;
            }
            return -1;
        }

        static Boxer ReadBoxer()
        {
            Console.WriteLine("Enter full name");
            string fullName = Console.ReadLine();
            if (fullName.Length == 0)
            {
                Console.WriteLine("The name must not be empty");
                return null;
            }
            Console.WriteLine("Enter identifier");
            string identifier = Console.ReadLine();
            if (fullName.Length == 0)
            {
                Console.WriteLine("The identifier must not be empty");
                return null;
            }
            else if (FindBoxer(identifier) != -1)
            {
                Console.WriteLine("The identifier must be individual");
                return null;
            }
            Console.WriteLine("Enter left hand strength");
            if (!double.TryParse(Console.ReadLine(), out double lhs) || lhs < 0)
            {
                Console.WriteLine("Error");
                return null;
            }
            Console.WriteLine("Enter right hand strength");
            if (!double.TryParse(Console.ReadLine(), out double rhs) || rhs < 0)
            {
                Console.WriteLine("Error");
                return null;
            }
            return new Boxer(fullName, identifier, lhs, rhs);
        }

        static int ReadInt(int n)
        {
            int action;
            while (!int.TryParse(Console.ReadLine(), out action) || action < 1 || action > n)
            {
                Console.WriteLine("Error! Try again");
            }
            return action;
        }
    }
}

