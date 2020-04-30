using System;
using System.Collections.Generic;
using System.Globalization;

namespace HumanProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Sportsman sportsman = new Boxer("Mike Tyson", "0",
                                            70, 178, 1500, 1500);
            while (true)
            {
                string s = sportsman.GetType().ToString();
                Console.WriteLine(s.Remove(0, s.IndexOf('.') + 1));
                Console.WriteLine(sportsman);
                Console.WriteLine("Enter:\n" +
                                  "1. Change current sportsman\n" +
                                  "2. Change menu\n" +
                                  "3. Take a meal\n" +
                                  "4. Train\n" +
                                  "5. Show menu\n" +
                                  "6. Exit");
                int action = ReadInt(6);
                if (action == 1)
                {
                    Console.WriteLine("Enter:\n" +
                                      "1. Sportsman\n" +
                                      "2. Runner\n" +
                                      "3. HockeyPlayer\n" +
                                      "4. Boxer\n" +
                                      "5. Exit");
                    action = ReadInt(5);
                    if (action == 1)
                    {
                        Sportsman newSportsman = ReadSportsman();
                        if (newSportsman != null) sportsman = newSportsman;
                    }
                    else if (action == 2)
                    {
                        Sportsman newSportsman = ReadRunner();
                        if (newSportsman != null) sportsman = newSportsman;
                    }
                    else if (action == 3)
                    {
                        Sportsman newSportsman = ReadHockeyPlayer();
                        if (newSportsman != null) sportsman = newSportsman;
                    }
                    else if (action == 4)
                    {
                        Sportsman newSportsman = ReadBoxer();
                        if (newSportsman != null) sportsman = newSportsman;
                    }
                    else continue;
                }
                else if (action == 2)
                {
                    Console.WriteLine("Enter:\n" +
                                      "1. Change breakfast\n" +
                                      "2. Change lunch\n" +
                                      "3. Change dinner\n" +
                                      "4. Exit");
                    int num = ReadInt(4);
                    if (num == 4) continue;
                    Console.WriteLine("Enter food:\n" +
                                      "1. Eggs\n" +
                                      "2. CottageCheese\n" +
                                      "3. Buckwheat\n" +
                                      "4. Beef\n" +
                                      "5. Fruits\n" +
                                      "6. Hamburger\n" +
                                      "7. Exit");
                    int food = ReadInt(7);
                    if (food == 7) continue;
                    Console.WriteLine("Enter drink:\n" +
                                      "1. Water\n" +
                                      "2. Juice\n" +
                                      "3. Tea\n" +
                                      "4. CocaCola\n" +
                                      "5. SparklingWater\n" +
                                      "6. Exit");
                    int drink = ReadInt(6);
                    if (drink == 6) continue;
                    sportsman.ChangeMenu(num, (SportsFood)food, (SportsDrinks)drink);
                }
                else if (action == 3)
                {
                    Console.WriteLine("Enter:\n" +
                                      "1. Breakfast\n" +
                                      "2. Lunch\n" +
                                      "3. Dinner\n" +
                                      "4. Exit");
                    int num = ReadInt(4);
                    if (num == 4) continue;
                    sportsman.HaveMeal(num);
                }
                else if (action == 4)
                {
                    Console.WriteLine("Enter minutes");
                    int minutes = ReadInt(12 * 60);
                    sportsman.Train(minutes);
                }
                else if (action == 5)
                {
                    Console.WriteLine("Menu:");
                    Console.WriteLine(string.Format("Breakfast: {0}, {1}\n" + 
                                                    "Lunch: {2}, {3}\n" +
                                                    "Dinner: {4}, {5}",
                                      sportsman.Menu.Breakfast.Food, sportsman.Menu.Breakfast.Drink,
                                      sportsman.Menu.Lunch.Food, sportsman.Menu.Lunch.Drink,
                                      sportsman.Menu.Dinner.Food, sportsman.Menu.Dinner.Drink));
                    Console.WriteLine();
                }
                else continue;
            }
        }

        static Sportsman ReadSportsman()
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
            Console.WriteLine("Enter weight");
            if (!double.TryParse(Console.ReadLine(), out double weight) || weight < 0)
            {
                Console.WriteLine("Error");
                return null;
            }
            Console.WriteLine("Enter height");
            if (!double.TryParse(Console.ReadLine(), out double height) || height < 0)
            {
                Console.WriteLine("Error");
                return null;
            }
            return new Sportsman(fullName, identifier, weight, height);
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
            Console.WriteLine("Enter weight");
            if (!double.TryParse(Console.ReadLine(), out double weight) || weight < 0)
            {
                Console.WriteLine("Error111");
                return null;
            }
            Console.WriteLine("Enter height");
            if (!double.TryParse(Console.ReadLine(), out double height) || height < 0)
            {
                Console.WriteLine("Error");
                return null;
            }
            Console.WriteLine("Enter speed");
            if (!double.TryParse(Console.ReadLine(), out double speed) || speed < 0)
            {
                Console.WriteLine("Error");
                return null;
            }
            return new Runner(fullName, identifier, weight, height, speed);
        }

        static HockeyPlayer ReadHockeyPlayer()
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
            Console.WriteLine("Enter weight");
            if (!double.TryParse(Console.ReadLine(), out double weight) || weight < 0)
            {
                Console.WriteLine("Error");
                return null;
            }
            Console.WriteLine("Enter height");
            if (!double.TryParse(Console.ReadLine(), out double height) || height < 0)
            {
                Console.WriteLine("Error");
                return null;
            }
            Console.WriteLine("Enter speed");
            if (!double.TryParse(Console.ReadLine(), out double speed) || speed < 0)
            {
                Console.WriteLine("Error");
                return null;
            }
            Console.WriteLine("Enter strike power");
            if (!double.TryParse(Console.ReadLine(), out double strikePower) || strikePower < 0)
            {
                Console.WriteLine("Error");
                return null;
            }
            return new HockeyPlayer(fullName, identifier, weight, height, speed, strikePower);
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
            Console.WriteLine("Enter weight");
            if (!double.TryParse(Console.ReadLine(), out double weight) || weight < 0)
            {
                Console.WriteLine("Error");
                return null;
            }
            Console.WriteLine("Enter height");
            if (!double.TryParse(Console.ReadLine(), out double height) || height < 0)
            {
                Console.WriteLine("Error");
                return null;
            }
            Console.WriteLine("Enter left hand strength");
            if (!double.TryParse(Console.ReadLine(), out double lhs) || lhs < 0)
            {
                Console.WriteLine("Error");
                return null;
            }
            Console.WriteLine("Enter rigth hand strength");
            if (!double.TryParse(Console.ReadLine(), out double rhs) || rhs < 0)
            {
                Console.WriteLine("Error");
                return null;
            }
            return new Boxer(fullName, identifier, weight, height, lhs, rhs);
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

