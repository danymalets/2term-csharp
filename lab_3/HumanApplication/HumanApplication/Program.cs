using System;
using System.Collections.Generic;
using System.Globalization;

namespace HumanApplication
{
    class Program
    {
        readonly static List<Human> humans = new List<Human>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter:\n" +
                                  "1. Show full list\n" +
                                  "2. Add new Human (SIMPLE)\n" +
                                  "3. Add new Human (FULL)\n" +
                                  "4. Find human\n" +
                                  "5. Remove human from list\n" +
                                  "6. Sort by name\n" +
                                  "7. Add parent-son relationship\n" +
                                  "8. Arrange a marriage\n" +
                                  "9. Make a divorce\n" +
                                  "10. Exit\n");
                int action;
                while (!int.TryParse(Console.ReadLine(), out action) || action < 1 || action > 10)
                {
                    Console.WriteLine("\nError! Try again\n");
                }
                Console.WriteLine();
                string identifier;
                Human human, parent, child, human1, human2;
                switch (action)
                {
                    case 1:
                        if (humans.Count == 0)
                        {
                            Console.WriteLine("List is empty");
                        }
                        else
                        {
                            Console.WriteLine("Full list:");
                            for (int i = 0; i < humans.Count; i++)
                            {
                                Console.WriteLine();
                                Console.Write(i + 1 + ". ");
                                humans[i].Write();
                            }
                        }
                        break;
                    case 2:
                        human = ReadHuman(true);
                        if (human == null)
                        {
                            Console.WriteLine("Error!");
                        }
                        else
                        {
                            Console.WriteLine("Successfully!");
                            humans.Add(human);
                        }
                        break;
                    case 3:
                        human = ReadHuman(false);
                        if (human == null)
                        {
                            Console.WriteLine("Error!");
                        }
                        else
                        {
                            Console.WriteLine("Successfully!");
                            humans.Add(human);
                        }
                        break;
                    case 4:
                        Console.WriteLine("Enter identifier");
                        identifier = Console.ReadLine();

                        human = FindHuman(identifier);

                        Console.WriteLine();
                        if (human == null)
                        {
                            Console.WriteLine("Human not found!");
                        }
                        else
                        {
                            Console.WriteLine("Human found:");
                            human.Write();
                        }
                        break;
                    case 5:
                        Console.WriteLine("Enter identifier");
                        identifier = Console.ReadLine();

                        human = FindHuman(identifier);

                        Console.WriteLine();
                        if (human == null)
                        {
                            Console.WriteLine("Human not found!");
                        }
                        else
                        {
                            human.Delete();
                            humans.Remove(human);
                            Console.WriteLine("Human successfully removed");
                        }
                        break;
                    case 6:
                        humans.Sort(new Human.FullNameComparer());
                        Console.WriteLine("Successfully!");
                        break;
                    case 7:
                        Console.WriteLine("Enter parent identifier");
                        identifier = Console.ReadLine();
                        parent = FindHuman(identifier);
                        if (parent == null)
                        {
                            Console.WriteLine("Parent not found!");
                            break;
                        }

                        Console.WriteLine("Enter child identifier");
                        identifier = Console.ReadLine();
                        child = FindHuman(identifier);
                        if (child == null)
                        {
                            Console.WriteLine("Child not found!");
                            break;
                        }

                        if (child.SetParent(parent))
                        {
                            Console.WriteLine("Successfully!");
                        }
                        else
                        {
                            if (parent.IsMale)
                                Console.WriteLine("Error! This child already has a male parent");
                            else
                                Console.WriteLine("Error! This child already has a female parent");
                        }
                        break;
                    case 8:
                        Console.WriteLine("Enter first human identifier");
                        identifier = Console.ReadLine();

                        human1 = FindHuman(identifier);
                        if (human1 == null)
                        {
                            Console.WriteLine("Human not found!");
                            break;
                        }
                        else if (human1.Partner != null)
                        {
                            Console.WriteLine("This human already has a partner");
                            break;
                        }

                        Console.WriteLine("Enter second human identifier");
                        identifier = Console.ReadLine();
                        human2 = FindHuman(identifier);
                        if (human2 == null)
                        {
                            Console.WriteLine("Human not found!");
                            break;
                        }
                        else if (human2.Partner != null)
                        {
                            Console.WriteLine("This human already has a partner");
                            break;
                        }

                        if (Human.Marriage(human1, human2))
                        {
                            Console.WriteLine("Successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Same-sex marriage is prohibited!");
                        }
                        break;
                    case 9:
                        Console.WriteLine("Enter the identifier of one of the partners");
                        identifier = Console.ReadLine();
                        human = FindHuman(identifier);
                        if (human == null)
                        {
                            Console.WriteLine("Human not found!");
                            break;
                        }
                        else if (human.Partner == null)
                        {
                            Console.WriteLine("This human has no partner");
                            break;
                        }
                        human.Divorce();
                        Console.WriteLine("Successfully!");
                        break;
                    case 10:
                        return;
                }
                Console.WriteLine();
            }
        }

        static Human ReadHuman(bool isSimpleAdd)
        {
            Console.WriteLine("Enter full name");
            string fullName = Console.ReadLine();
            if (fullName.Length == 0)
            {
                Console.WriteLine("\nThe name must not be empty");
                return null;
            }

            Console.WriteLine("Enter identifier");
            string identifier = Console.ReadLine();
            if (fullName.Length == 0)
            {
                Console.WriteLine("\nThe identifier must not be empty");
                return null;
            }
            else if (FindHuman(identifier) != null)
            {
                Console.WriteLine("\nThe identifier must be individual");
                return null;
            }

            Console.WriteLine("Enter gender (M - male, F - female)");
            string strGender = Console.ReadLine();
            bool isMale;
            if (strGender.ToUpper() == "M")
            {
                isMale = true;
            }
            else if (strGender.ToUpper() == "F")
            {
                isMale = false;
            }
            else
            {
                Console.WriteLine("\nWrong format");
                return null;
            }

            if (isSimpleAdd)
            {
                return new Human(fullName, identifier, isMale);
            }

            Console.WriteLine("Enter date of birth (DD.MM.YYYY)");
            string strDate = Console.ReadLine();
            if (!DateTime.TryParseExact(strDate, "dd.MM.yyyy", new CultureInfo("en-US"),
                DateTimeStyles.None, out DateTime dateOfBirth))
            {
                Console.WriteLine("\nWrong format");
                return null;
            }
            else if (Human.GetAge(dateOfBirth) < 0)
            {
                Console.WriteLine("\nAge cannot be less than zero");
                return null;
            }

            Console.WriteLine("Enter mother identifier or leave a blank line");
            string motherIdentifier = Console.ReadLine();
            Human mother;
            if (motherIdentifier == "")
            {
                mother = null;
            }
            else
            {
                mother = FindHuman(motherIdentifier);
                if (mother == null)
                {
                    Console.WriteLine("\nMother not found");
                    return null;
                }
                else if (mother.IsMale)
                {
                    Console.WriteLine("\nThis human is a male");
                    return null;
                }
            }

            Console.WriteLine("Enter father identifier or leave a blank line");
            string fatherIdentifier = Console.ReadLine();
            Human father;
            if (fatherIdentifier == "")
            {
                father = null;
            }
            else
            {
                father = FindHuman(fatherIdentifier);
                if (father == null)
                {
                    Console.WriteLine("\nFather not found");
                    return null;
                }
                else if (!father.IsMale)
                {
                    Console.WriteLine("\nThis human is a female");
                    return null;
                }
            }

            Console.WriteLine();
            return new Human(fullName, identifier, isMale, dateOfBirth, mother, father);
<<<<<<< HEAD
=======

>>>>>>> f1b13ccf061a40115c70221048a27296f30346f2
        }

        static Human FindHuman(string identifier)
        {
            for (int i = 0; i < humans.Count; i++)
            {
                if (humans[i].Identifier == identifier)
                {
                    return humans[i];
                }
            }
            return null;
        }
    }
}

