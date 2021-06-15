using System;
using System.Collections.Generic;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            UserInputReader userInput = new();
            Commander commander = new();

            Console.WriteLine("Welcome to the Visma book library!\nWhat would you like to do?");

            do
            {
                // User gets to choose a library activity from a list
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
                        if (readerName == null) // Reader already has 3 borrowed books
                            break;

                        bookName = userInput.BookNameToBorrow();
                        if (bookName == "exit") // User's decision to stop the action
                            break;

                        DateTime returnDate = userInput.ReturnDate();
                        commander.BorrowBook(bookName, readerName, returnDate);
                        Console.WriteLine(bookName + " has been borrowed to " + readerName);
                        break;

                    case 3:
                        Console.WriteLine("\nYou have chosen returning a book. Please enter information about your order:");

                        readerName = userInput.ReaderToReturnBook();

                        List<Book> readerBooks = commander.ListBooks(9, readerName);
                        if(readerBooks.Count == 0)
                        {
                            Console.WriteLine("\nYou don't have any borrowed books. Try borrowing one.");
                            break;
                        }

                        bookName = userInput.BookNameToReturn(readerBooks);

                        commander.ReturnBook(bookName, readerName);
                        Console.WriteLine(bookName + " has been returned");
                        break;

                    case 4:
                        Console.WriteLine("\nYou have chosen listing books. Please choose a filter: ");

                        int filter = userInput.Filter();
                        string filterLabel = userInput.FilterLabel(filter);
                        List<Book> filteredBooks = commander.ListBooks(filter, filterLabel);

                        if (filteredBooks.Count != 0)
                        {
                            Console.WriteLine("\n" + $"{"Name",-20} {"Author",-20} {"Category",-15} {"Language",-15} " +
                                              $"{"Publication date",-20} {"ISBN",-15}");

                            foreach (Book book in filteredBooks)
                                Console.WriteLine($"{book.Name,-20} {book.Author,-20} {book.Category,-15} {book.Language,-15} " +
                                                  $"{book.PublicationDate,-20:yyyy-MM-dd} {book.ISBN,-15}");
                        }
                        else
                        {
                            Console.WriteLine("\nEntered filter doesn't match any books in the library.");
                        }
                        break;

                    default:
                        break;
                }
            } while (userInput.MoreActions());
        }
    }
}
