using System;
using System.Collections.Generic;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            InputReader input = new();
            bool moreActions;

            Console.WriteLine("Welcome to the Visma book library!\nWhat would you like to do?");

            do
            {
                moreActions = false;

                Console.WriteLine("\n1 - Add a new book to the library");
                Console.WriteLine("2 - Borrow a book");
                Console.WriteLine("3 - Return a book");
                Console.WriteLine("4 - Filter the book list");
                Console.WriteLine("5 - Remove a book from the library");

                Console.Write("\nPlease enter a matching number: ");
                int option = input.Option();

                Service service = new();
                string bookName, reader;
                switch (option)
                {
                    case 1:
                        Console.WriteLine("\nYou have chosen adding a new book. Please enter information about the book:");
                        service.AddNewBook(input.BookInfo());
                        Console.WriteLine("A new book has been added.");
                        break;
                    case 2:
                        Console.WriteLine("\nYou have chosen borrowing a book. Maximum period of keeping the book is 2 months.\n" +
                                          "Please enter information about the order:");

                        reader = input.ReaderToBorrowBook();
                        if (reader == null)
                            break;

                        bookName = input.BookNameToBorrow();
                        if (bookName == "exit")
                            break;

                        DateTime returnDate = input.ReturnDate();
                        service.BorrowBook(bookName, reader, returnDate);
                        Console.WriteLine(bookName + " has been borrowed to " + reader);
                        break;
                    case 3:
                        Console.WriteLine("\nYou have chosen returning a book. Please enter information about your order:");

                        reader = input.ReaderToReturnBook();
                        if (reader == null)
                            break;

                        List<Book> readerBooks = service.GetReaderBooks(reader);
                        if(readerBooks.Count == 0)
                        {
                            Console.WriteLine("\nYou don't have any borrowed books. Try borrowing one.");
                            break;
                        }

                        bookName = input.BookNameToReturn(readerBooks);

                        service.ReturnBook(bookName, reader);
                        Console.WriteLine(bookName + " has been returned");
                        break;
                    default:
                        break;
                }

                moreActions = input.MoreActions();
            } while (moreActions);
        }
    }
}
