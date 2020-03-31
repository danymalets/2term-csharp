using System;
using System.Collections.Generic;

namespace HumanApplication
{
    public class Human
    {
        public string FullName { get; private set; }
        public string Identifier { get; private set; }
        public bool IsMale { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public Human Partner { get; private set; }
        public Human Mother { get; private set; }
        public Human Father { get; private set; }
        readonly private List<Human> Children = new List<Human>();

        public Human(string fullName, string identifier, bool isMale)
        {
            FullName = fullName;
            Identifier = identifier;
            IsMale = isMale;
            DateOfBirth = DateTime.Now.AddYears(-18);
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

        public void Write()
        {
            int age = GetAge(DateOfBirth);
            Console.Write(FullName + ", id = " + Identifier + ", " + age);
            if (age == 1) Console.Write(" year, ");
            else Console.Write(" years, ");

            if (IsMale) Console.WriteLine("Male");
            else Console.WriteLine("Female");

            if (IsMale)
            {
                if (Partner == null) Console.Write("no wife");
                else Console.Write("Wife - " + Partner.FullName + " [id=" + Partner.Identifier + "] ");
            }
            else
            {
                if (Partner == null) Console.Write("no husband");
                else Console.Write("Husband - " + Partner.FullName + " [id=" + Partner.Identifier + "] ");
            }
            Console.Write(", ");

            if (Mother == null) Console.Write("no mother");
            else Console.Write("Mother - " + Mother.FullName + " [id=" + Mother.Identifier + "] ");
            Console.Write(", ");

            if (Father == null) Console.Write("no father");
            else Console.Write("Father - " + Father.FullName + " [id=" + Father.Identifier + "] ");
            Console.WriteLine();

            if (Children.Count == 0) Console.WriteLine("no children");
            else Console.WriteLine("Children list:");
            for (int i = 0; i < Children.Count; i++)
            {
                Console.WriteLine(i + 1 + ") " + Children[i].FullName +
                    " [id=" + Children[i].Identifier + "]");
            }
        }

        public class FullNameComparer : IComparer<Human>
        {
            public int Compare(Human h1, Human h2)
            {
                return string.Compare(h1.FullName, h2.FullName);
            }
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
