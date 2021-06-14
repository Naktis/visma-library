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
            string name = ReadString("Name: ");
            string author = ReadString("Author: ");
            string category = ReadString("Category: ");
            string language = ReadString("Language: ");

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

            string isbn = ReadString("ISBN: ");

            return new Book(name, author, category, language, date, isbn);
        }

        public string ReaderToBorrowBook()
        {
            string name = ReadString("Your name: ");

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

        public string ReaderToReturnBook()
        {
            return ReadString("Your name: ");
        }

        public string BookNameToBorrow()
        {
            Console.Write("Book name: ");
            string name;
            do
            {
                name = Console.ReadLine();
                if (_validator.BookExistsAvailable(name) || name == "exit")
                    break;
                else
                    Console.Write("This book hasn't been added to the library. " +
                                  "Try another book name or exit the action (type exit):");
            } while (true);

            return name;
        }

        public string BookNameToReturn(List<Book> books)
        {
            Console.WriteLine("\nYou have borrowed the following books: ");

            foreach(Book book in books)
            {
                Console.WriteLine(book.Name);
            }

            Console.Write("\nEnter the name of the book you want to return: ");
            Book bookMatch;
            string name;
            do
            {
                name = Console.ReadLine();
                bookMatch = books.FirstOrDefault(x => x.Name == name);

                if (bookMatch != null)
                    break;
                else
                    Console.Write("You haven't borrowed this book. Try again: ");
            } while (true);

            if (DateTime.Today > bookMatch.ReturnDate)
                Console.WriteLine("\nBetter late than never, huh?");

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
            if (ReadString("\nDo you want to do more things in the library? (y/n) ") == "y")
                return true;
            else
                return false;
        }

        private string ReadString(string label)
        {
            Console.Write(label);
            return Console.ReadLine();
        }
    }
}
