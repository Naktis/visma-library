using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class InputReader
    {
        private readonly Service _validator;

        public InputReader()
        {
            _validator = new Service();
        }

        public int Option()
        {
            int option;
            do
            {
                // Converts the input into int if possible (if not - returns false)
                if (int.TryParse(Console.ReadLine(), out option))
                {
                    if (option >= 1 && option <= 5)
                    {
                        break;
                    }
                }
                Console.Write("This option doesn't exist. Please enter a valid number: ");
            } while (true);
            return option;
        }

        public Book BookInfo()
        {
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Author: ");
            string author = Console.ReadLine();
            Console.Write("Category: ");
            string category = Console.ReadLine();
            Console.Write("Language: ");
            string language = Console.ReadLine();

            Console.Write("Publication date (YYYY-MM-DD): ");
            DateTime date;
            do
            {
                if (DateTime.TryParse(Console.ReadLine(), out date))
                {
                    break;
                }
                Console.Write("Entered date is invalid. Please try again: ");
            } while (true);

            Console.Write("ISBN: ");
            string isbn = Console.ReadLine();

            return new Book(name, author, category, language, date, isbn);
        }

        public string Reader()
        {
            Console.Write("Your name: ");
            string name = Console.ReadLine();

            if (!_validator.BookLimitReached(name))
            {
                return name;
            }
            else
            {
                Console.WriteLine("Taking more than 3 books isn't allowed. Please return a book and try again.");
                return null;
            }
        }
        public string BookName()
        {
            string name;
            Console.Write("Book name: ");

            do
            {
                name = Console.ReadLine();
                if (_validator.BookExistsAvailable(name) || name == "exit")
                    break;
                else
                    Console.WriteLine("This book hasn't been added to the library. " +
                                      "Try another book name or exit the action (type exit):");
            } while (true);

            return name;
        }
        public DateTime ReturnDate()
        {
            int maxMonth = (DateTime.Today.Month + 2) % 12;

            Console.Write("Days to borrow the book for: ");

            DateTime returnDate;
            do
            {
                if (int.TryParse(Console.ReadLine(), out int days)) // Number-only input check
                {
                    returnDate = DateTime.Today.AddDays(days);
                    if (days >= 1)
                    {
                        if (returnDate.Month < maxMonth)
                        {
                            break;
                        }
                        else if (returnDate.Month == maxMonth && returnDate.Day <= DateTime.Today.Day)
                        {
                            break;
                        }
                    }
                }
                Console.Write("Entered date is invalid. Please try again: ");
            } while (true);

            return returnDate;
        }

        public bool MoreActions()
        {
            Console.Write("Do you want to do more things in the library? (y/n) ");

            if (Console.ReadLine() == "y")
                return true;
            else
                return false;
        }
    }
}
