using System;
using System.Collections.Generic;

namespace HumanProject
{
    public class Human : IComparable<Human>
    {
        public string FullName { get; private set; }
        public string Identifier { get; private set; }
        public bool IsMale { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public Human Partner { get; private set; }
        public Human Mother { get; private set; }
        public Human Father { get; private set; }
        public List<Human> Children = new List<Human>();

        public Human()
        {
            FullName = "-";
            Identifier = "-";
            DateOfBirth = DateTime.Now;
        }

        public Human(string fullName, string identifier)
        {
            FullName = fullName;
            Identifier = identifier;
            IsMale = true;
            DateOfBirth = DateTime.Now;
        }

        public Human(string fullName, string identifier, bool isMale)
        {
            FullName = fullName;
            Identifier = identifier;
            IsMale = isMale;
            DateOfBirth = DateTime.Now;
        }

        public Human(string fullName, string identifier, bool isMale,
            DateTime dateOfBirth, Human mother, Human father)
        {
            FullName = fullName;
            Identifier = identifier;
            IsMale = isMale;
            DateOfBirth = dateOfBirth;
            Mother = mother;
            Father = father;
            if (Mother != null) Mother.AddChild(this);
            if (Father != null) Father.AddChild(this);
        }

        void AddChild(Human child)
        {
            Children.Add(child);
        }

        void RemoveChild(Human child)
        {
            Children.Remove(child);
        }

        public bool SetParent(Human parent)
        {
            if (parent.IsMale)
            {
                if (Father != null) return false;
                Father = parent;
            }
            else
            {
                if (Mother != null) return false;
                Mother = parent;
            }
            parent.AddChild(this);
            return true;
        }

        public override string ToString()
        {
            string res = "";
            int age = GetAge(DateOfBirth);
            res += $"{FullName}, id = {Identifier}, {age}";
            if (age == 1) res += " year, ";
            else res += " years, ";

            if (IsMale) res += "Male\n";
            else res += "Female\n";

            if (IsMale)
            {
                if (Partner == null) res += "no wife";
                else res += $"Wife - {Partner.FullName} [id={Partner.Identifier}] ";
            }
            else
            {
                if (Partner == null) res += "no husband";
                else res += $"Husband - {Partner.FullName} [id={Partner.Identifier}] ";
            }
            res += ", ";

            if (Mother == null) res += "no mother";
            else res += $"Mother - {Mother.FullName} [id={Mother.Identifier}]";

            res += ", ";

            if (Father == null) res += "no father";
            else res += $"Father - {Father.FullName} [id={Father.Identifier}]";
            res += "\n";

            if (Children.Count == 0) res += "no children";
            else res += "Children list:\n";
            for (int i = 0; i < Children.Count; i++)
            {
                res += $"{i + 1}) {Children[i].FullName} [id={Children[i].Identifier}]\n";
            }
            return res;
        }

        public int CompareTo(Human human)
        {
            return -DateOfBirth.CompareTo(human.DateOfBirth);
        }

        public static bool Marriage(Human human1, Human human2)
        {
            if (human1.IsMale == human2.IsMale) return false;
            human1.Partner = human2;
            human2.Partner = human1;
            return true;
        }

        public void Divorce()
        {
            Partner.Partner = null;
            Partner = null;
        }

        public static int GetAge(DateTime dateOfBirth)
        {
            int age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear) age--;
            return age;
        }

        public void Delete()
        {
            if (Partner != null) Partner.Partner = null;
            if (Mother != null) Mother.RemoveChild(this);
            if (Father != null) Father.RemoveChild(this);
            if (IsMale)
            {
                foreach (var child in Children)
                {
                    child.Father = null;
                }
            }
            else
            {
                foreach (var child in Children)
                {
                    child.Mother = null;
                }
            }
        }
    }
}

