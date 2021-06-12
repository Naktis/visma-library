using System;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            InputReader input = new();

            Console.WriteLine("Welcome to the Visma book library!\nWhat would you like to do?\n");
            Console.WriteLine("1 - Add a new book to the library");
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
                default:
                    break;
            }
        }
    }
}
