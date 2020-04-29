using System;

namespace HumanProject
{
    public enum SportsFood
    {
        Eggs = 1,
        CottageCheese,
        Buckwheat,
        Beef,
        Fruits,
        Hamburger
    }

    public enum SportsDrinks
    {
        Water = 1,
        Juice,
        Tea,
        CocaCola,
        SparklingWater
    }

    public struct SportsMeal
    {
        public SportsFood Food;
        public SportsDrinks Drink;
    }

    public struct SportsMenu
    {
        public SportsMeal Breakfast, Lunch, Dinner;
    }

    public class Sportsman : Human
    {
        public double Weight
        {
            get
            {
                return _weight;
            }
            set
            {
                if (value < 0) _weight = 0;
                else if (value > 150) _weight = 150;
                else _weight = value;
            }
        }
        double _weight;

        public double Height
        {
            get
            {
                return _height;
            }
            set
            {
                if (value < 0) _height = 0;
                else if (value > 250) _height = 250;
                else _height = value;
            }
        }
        double _height;

        public SportsMenu Menu = new SportsMenu()
        {
            Breakfast = new SportsMeal()
            {
                Food = SportsFood.Eggs,
                Drink = SportsDrinks.Water
            },
            Lunch = new SportsMeal()
            {
                Food = SportsFood.Beef,
                Drink = SportsDrinks.Tea
            },
            Dinner = new SportsMeal()
            {
                Food = SportsFood.Fruits,
                Drink = SportsDrinks.Juice
            }
        };

        public Sportsman() : base()
        {
            Weight = 75;
            Height = 180;
        }

        public Sportsman(string fullName, string identifier) : base(fullName, identifier) {
            Weight = 75;
            Height = 180;
        }

        public Sportsman(string fullName, string identifier, double weight, double height)
            : base(fullName, identifier)
        {
            Weight = weight;
            Height = height;
        }

        public override string ToString()
        {
            return string.Format("{0} id={1} w={2} h={3}", FullName, Identifier, Weight, Height);
        }

        public void ChangeMenu(int mealNum, SportsFood food, SportsDrinks drink)
        {
            switch (mealNum)
            {
                case 1: ChangeMenu(Menu.Breakfast, food, drink); return;
                case 2: ChangeMenu(Menu.Lunch, food, drink); return;
                case 3: ChangeMenu(Menu.Dinner, food, drink); return;
                default: throw new Exception("Wrong meal number");
            }
        }

        void ChangeMenu(SportsMeal meal, SportsFood food, SportsDrinks drink)
        {
            meal.Food = food;
            meal.Drink = drink;
        }

        public void HaveMeal(int mealNum)
        {
            switch (mealNum)
            {
                case 1: HaveMeal(Menu.Breakfast); return;
                case 2: HaveMeal(Menu.Lunch); return;
                case 3: HaveMeal(Menu.Dinner); return;
                default: throw new Exception("Wrong meal number");
            }
        }

        public void HaveMeal(SportsMeal meal)
        {
            switch (meal.Food)
            {
                case SportsFood.Eggs:
                    Weight += 0.12;
                    Height += 0.04;
                    break;
                case SportsFood.CottageCheese:
                    Weight += 0.23;
                    Height += 0.09;
                    break;
                case SportsFood.Buckwheat:
                    Weight += 0.23;
                    Height += 0.04;
                    break;
                case SportsFood.Beef:
                    Weight += 0.31;
                    Height += 0.05;
                    break;
                case SportsFood.Fruits:
                    Weight += 0.11;
                    Height += 0.06;
                    break;
                case SportsFood.Hamburger:
                    Weight += 0.41;
                    Height += 0.10;
                    break;
                default:
                    break;
            }
            switch (meal.Drink)
            {
                case SportsDrinks.Water:
                    Weight += 0.1;
                    Height += 0.0001;
                    break;
                case SportsDrinks.Juice:
                    Weight += 0.2;
                    Height += 0.0003;
                    break;
                case SportsDrinks.Tea:
                    Weight += 0.1;
                    Height += 0.0002;
                    break;
                case SportsDrinks.CocaCola:
                    Weight += 0.5;
                    Height += 0.0001;
                    break;
                case SportsDrinks.SparklingWater:
                    Weight += 0.3;
                    Height += 0.0003;
                    break;
                default:
                    break;
            }
        }

        public virtual void Train(int minutes)
        {
            Random random = new Random();
            Weight -= 0.001 * minutes * random.Next(1, 11);
            Height += 0.0001 * minutes * random.Next(-1, 3);
        }
    }
}
