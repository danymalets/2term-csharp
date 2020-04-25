using System;
namespace HumanProject
{
    public enum SportsFood
    {
        Eggs,
        CottageCheese,
        Buckwheat,
        Beef,
        Fruits,
        Hamburger
    }

    public enum SportsDrinks
    {
        Water,
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
                if (value < 50) _weight = 50;
                else if (value > 130) _weight = 130;
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
                if (value < 150) _height = 150;
                else if (value > 210) _height = 210;
                else _height = value;
            }
        }
        double _height;

        SportsMenu menu;

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

        public void ChangeMenu(int mealNum, SportsFood food, SportsDrinks drink)
        {
            switch (mealNum)
            {
                case 1: ChangeMenu(menu.Breakfast, food, drink); return;
                case 2: ChangeMenu(menu.Lunch, food, drink); return;
                case 3: ChangeMenu(menu.Dinner, food, drink); return;
                default: throw new Exception("Wrong meal number");
            }
        }

        void ChangeMenu(SportsMeal meal, SportsFood food, SportsDrinks drink)
        {
            meal.Food = food;
            meal.Drink = drink;
        }

        public void TakeMeal(int mealNum)
        {
            switch (mealNum)
            {
                case 1: TakeMeal(menu.Breakfast); return;
                case 2: TakeMeal(menu.Lunch); return;
                case 3: TakeMeal(menu.Dinner); return;
                default: throw new Exception("Wrong meal number");
            }
        }

        public void TakeMeal(SportsMeal meal)
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
                    break;
                case SportsDrinks.Juice:
                    Weight += 0.2;
                    break;
                case SportsDrinks.Tea:
                    Weight += 0.1;
                    break;
                case SportsDrinks.CocaCola:
                    Weight += 0.5;
                    break;
                case SportsDrinks.SparklingWater:
                    Weight += 0.3;
                    break;
                default:
                    break;
            }
        }

        public virtual void Train(int minutes)
        {
            Random random = new Random();
            Weight -= 0.001 * minutes * random.Next(1, 11);
            Height += 0.001 * minutes * random.Next(-1, 2);
        }
    }
}
