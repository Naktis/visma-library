using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class InputReader
    {
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
    }
}
