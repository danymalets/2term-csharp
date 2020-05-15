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
                                            98, 180, 900, 900);
            while (true)
            {
                string s = sportsman.GetType().ToString();
                Console.WriteLine(s.Remove(0, s.IndexOf('.') + 1));
                Console.WriteLine(sportsman);
                Console.WriteLine("Enter:\n" +
                                  "1. Change current sportsman\n" +
                                  "2. Change menu\n" +
                                  "3. Have a meal\n" +
                                  "4. Train\n" +
                                  "5. Show menu\n" +
                                  "6. Сhange parameter handlers\n" +
                                  "7. Change meal handlers\n" +
                                  "8. Exit");
                int action = ReadInt(7);
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
                                      "7. No food\n" +
                                      "8. Exit");
                    int food = ReadInt(8);
                    if (food == 8) continue;
                    Console.WriteLine("Enter drink:\n" +
                                      "1. Water\n" +
                                      "2. Juice\n" +
                                      "3. Tea\n" +
                                      "4. CocaCola\n" +
                                      "5. SparklingWater\n" +
                                      "6. No drink\n" +
                                      "7. Exit");
                    int drink = ReadInt(7);
                    if (drink == 7) continue;
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
                    try
                    {
                        sportsman.HaveMeal(num);
                    }
                    catch
                    {
                        Console.WriteLine("Nothing to eat or drink!");
                    }
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
                else if (action == 6)
                {
                    Console.WriteLine("Enter:\n" +
                                      "1. Show modified parameters\n" +
                                      "2. Show modified parameters in detail\n" +
                                      "3. Show nothing\n" +
                                      "4. Exit");
                    action = ReadInt(4);

                    if (action == 1)
                    {
                        sportsman.ParameterChangeMessage =
                            (parameter, prValue, value) => Console.WriteLine(parameter + " changed");
                    }
                    else if (action == 2)
                    {
                        sportsman.ParameterChangeMessage = delegate (string parameter, double prValue, double value)
                        {
                            string add = Math.Round(value - prValue, 4).ToString();
                            if (add[0] != '-') add = '+' + add;
                            Console.WriteLine(string.Format("{0} --- {1} -> ({2}) -> {3}", parameter, prValue, add, value));
                        };
                    }
                    else if (action == 4)
                    {
                        sportsman.ParameterChangeMessage = null;
                    }
                }
                else if (action == 7)
                {
                    Console.WriteLine("Enter:\n" +
                                      "1. Show food and drink at meal time\n" +
                                      "2. Remove messages at meal time\n" +
                                      "3. Add the message \"Bon appetit!\"\n" +
                                      "4. Remove the message \"Bon appetit!\"\n" +
                                      "5. Exit");
                    action = ReadInt(5);

                    Sportsman.MealHandler ShowMeal = delegate (SportsMeal meal)
                    {
                        Console.WriteLine(string.Format("Food = {0}, Drink = {1}", meal.Food, meal.Drink));
                    };

                    Sportsman.MealHandler ShowMessage = delegate (SportsMeal meal)
                    {
                        Console.WriteLine("Bon appetit!");
                    };
                    if (action == 1)
                    {
                        sportsman.MealEvent += ShowMeal;
                    }
                    else if (action == 2)
                    {
                        sportsman.MealEvent -= ShowMeal;
                    }
                    else if (action == 3)
                    {
                        sportsman.MealEvent += ShowMessage;
                    }
                    else if (action == 4)
                    {
                        sportsman.MealEvent -= ShowMessage;
                    }
                }
                else break;
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

