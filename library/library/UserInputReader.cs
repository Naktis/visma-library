using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class UserInputReader
    {
        private readonly ConstraintValidator _validator;

        public UserInputReader()
        {
            _validator = new();
        }

        public int Option()
        {
            Console.WriteLine("\n1 - Add a new book to the library");
            Console.WriteLine("2 - Borrow a book");
            Console.WriteLine("3 - Return a book");
            Console.WriteLine("4 - List and filter the book list");
            Console.WriteLine("5 - Remove a book from the library");

            Console.Write("\nPlease enter a matching number: ");

            int option;
            do
            {
                // Converts the input into int if possible (if not - returns false)
                if (int.TryParse(Console.ReadLine(), out option))
                    if (option >= 1 && option <= 5)
                        break;
                
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
                    break;
                
                Console.Write("Entered date is invalid. Please try again: ");
            } while (true);

            Console.Write("ISBN: ");
            string isbn;
            do
            {
                isbn = Console.ReadLine();
                if (isbn.All(char.IsDigit)) // Checks if all chars in entered string are digits
                    break;

                Console.Write("Entered code contains non-digit characters. Please enter a valid code: ");
            } while (true);

            return new Book(name, author, category, language, date, isbn);
        }

        public string ReaderToBorrowBook()
        {
            string name = ReadString("Your name: ");

            if (!_validator.BookLimitReached(name)) // Checks whether reader is eligible to borrow more books
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

        public string BookNameToReturn(List<Book> books)
        {
            Console.WriteLine("\nYou have borrowed the following books: ");

            foreach(Book book in books)
                Console.WriteLine(book.Name);

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
                        // Return month and max period month aren't the same
                        if (returnDate.Month < maxMonth)
                            break;
                       
                        // Return month and max period month match, but 2 month constraint isn't violated
                        else if (returnDate.Month == maxMonth && returnDate.Day <= DateTime.Today.Day)
                            break;
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

        public int Filter()
        {
            Console.WriteLine("\n1 - No filter");
            Console.WriteLine("2 - Filter by author");
            Console.WriteLine("3 - Filter by category");
            Console.WriteLine("4 - Filter by language");
            Console.WriteLine("5 - Filter by ISBN");
            Console.WriteLine("6 - Filter by name");
            Console.WriteLine("7 - Filter taken or available books");

            Console.Write("\nPlease enter a matching number: ");

            int option;
            do
            {
                if (int.TryParse(Console.ReadLine(), out option))
                    if (option >= 1 && option <= 7)
                        break;
                
                Console.Write("This option doesn't exist. Please enter a valid number: ");
            } while (true);

            if (option == 7) // Taken or available
            {
                Console.WriteLine("Please choose book availability");
                Console.WriteLine("\n1 - Available");
                Console.WriteLine("2 - Taken");

                do
                {
                    if (int.TryParse(Console.ReadLine(), out option))
                    {
                        if (option == 1) // Available
                        {
                            option = 7;  // This number will be used in commander class listing method
                            break;
                        }
                        if (option == 2) // Taken
                        {
                            option = 8;
                            break;
                        }
                    }
                    Console.Write("This option doesn't exist. Please enter a valid number: ");
                } while (true);
            }

            return option;
        }

        public string FilterLabel(int option)
        {
            if (option == 1 || option == 7 || option == 8) // No filter, available or taken
                return "";
            else
                return ReadString("Filter value: ");
        }

        public Book ChooseBook()
        {
            Console.Write("Book name: ");
            string name;
            
            name = Console.ReadLine();

            // Books that match the name and aren't taken
            List<Book> matchesByName = _validator.AvailableBooksByName(name);

            if (matchesByName.Count == 0)
            {
                Console.WriteLine("There are no available copies of this book at the library.");
                return null;
            }

            if (matchesByName.Count == 1)
                return matchesByName[0];

            Console.WriteLine("The following available books match your query:");
            Console.WriteLine("\n" + $"{"Index",-5} {"Name",-20} {"Author",-20} {"Category",-15} {"Language",-15} " +
                                              $"{"Publication date",-20} {"ISBN",-15}");
            int index = 1;
            foreach(Book b in matchesByName)
            {
                Console.WriteLine(index + "     " + $"{b.Name,-20} {b.Author,-20} {b.Category,-15} {b.Language,-15} " +
                                  $"{b.PublicationDate,-20:yyyy-MM-dd} {b.ISBN,-15}");
                index++;
            }
                
            Console.Write("Enter the index of the book you choose: ");
            int chosenIndex;
            do
            {
                if (int.TryParse(Console.ReadLine(), out chosenIndex))
                    if (chosenIndex >= 1 && chosenIndex <= index)
                        break;

                Console.Write("This option doesn't exist. Please enter a valid number: ");
            } while (true);

            return matchesByName[chosenIndex - 1];
        }

        private string ReadString(string label)
        {
            Console.Write(label);
            return Console.ReadLine();
        }
    }
}
