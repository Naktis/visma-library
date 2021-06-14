using System;
using System.Collections.Generic;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            UserInputReader userInput = new();
            ConstraintValidator validator = new();
            Commander commander = new();

            Console.WriteLine("Welcome to the Visma book library!\nWhat would you like to do?");

            do
            {
                Console.WriteLine("\n1 - Add a new book to the library");
                Console.WriteLine("2 - Borrow a book");
                Console.WriteLine("3 - Return a book");
                Console.WriteLine("4 - Filter the book list");
                Console.WriteLine("5 - Remove a book from the library");

                Console.Write("\nPlease enter a matching number: ");
                int option = userInput.Option();

                string bookName, readerName;
                switch (option)
                {
                    case 1:
                        Console.WriteLine("\nYou have chosen adding a new book. Please enter information about the book:");
                        commander.AddNewBook(userInput.BookInfo());
                        Console.WriteLine("A new book has been added.");
                        break;

                    case 2:
                        Console.WriteLine("\nYou have chosen borrowing a book. Maximum period of keeping the book is 2 months.\n" +
                                          "Please enter information about the order:");

                        readerName = userInput.ReaderToBorrowBook();
                        if (readerName == null)
                            break;

                        bookName = userInput.BookNameToBorrow();
                        if (bookName == "exit")
                            break;

                        DateTime returnDate = userInput.ReturnDate();
                        commander.BorrowBook(bookName, readerName, returnDate);
                        Console.WriteLine(bookName + " has been borrowed to " + readerName);
                        break;

                    case 3:
                        Console.WriteLine("\nYou have chosen returning a book. Please enter information about your order:");

                        readerName = userInput.ReaderToReturnBook();
                        if (readerName == null)
                            break;

                        List<Book> readerBooks = validator.GetReaderBooks(readerName);
                        if(readerBooks.Count == 0)
                        {
                            Console.WriteLine("\nYou don't have any borrowed books. Try borrowing one.");
                            break;
                        }

                        bookName = userInput.BookNameToReturn(readerBooks);

                        commander.ReturnBook(bookName, readerName);
                        Console.WriteLine(bookName + " has been returned");
                        break;

                    default:
                        break;
                }
            } while (userInput.MoreActions());
        }
    }
}
