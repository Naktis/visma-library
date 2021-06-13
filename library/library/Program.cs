using System;

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
                switch (option)
                {
                    case 1:
                        Console.WriteLine("You have chosen adding a new book. Please enter information about the book:");
                        service.AddNewBook(input.BookInfo());
                        Console.WriteLine("A new book has been added.");
                        break;
                    case 2:
                        Console.WriteLine("You have chosen borrowing a book. Maximum period of keeping the book is 2 months.\n" +
                                          "Please enter information about the order:");

                        string reader = input.Reader();
                        if (reader == null)
                            break;

                        string bookName = input.BookName();
                        if (bookName == "exit")
                            break;

                        DateTime returnDate = input.ReturnDate();
                        service.BorrowBook(bookName, reader, returnDate);
                        Console.WriteLine(bookName + " has been borrowed to " + reader);
                        break;
                    default:
                        break;
                }

                moreActions = input.MoreActions();
            } while (moreActions);
        }
    }
}
