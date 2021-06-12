using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Library;

namespace Library.Tests
{
    [TestClass]
    public class AddNewBookTests
    {
        [TestMethod]
        public void Add_NewBook_ToEmptyLibrary(Book book, List<Book> list) 
        {
            Service service = new();
            List<Book> booksCorrect = { book };

            service.AddNewBook(book);
            string jsonData = File.ReadAllText("../../../Data/books.json");
            List<Book> booksTest = JsonSerializer.Deserialize<List<Book>>(jsonData);

            Assert.Equal(booksTest, booksCorrect);
        }
    }
}
