using System;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            Service service = new Service();

            Book testBook = new Book { Name="Name1", Author="Author1", Category="Cat1", Language="Lang1", PublicationDate=DateTime.Today, ISBN="0000000000000" };
            Book testBook2 = new Book { Name = "Name2", Author = "Author2", Category = "Cat2", Language = "Lang2", PublicationDate = DateTime.Today, ISBN = "0000000000001" };

            service.AddNewBook(testBook2);
        }
    }
}
