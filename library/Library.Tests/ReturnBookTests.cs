using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Library.Tests
{
    public class ReturnBookTests
    {
        [Fact]
        public void ReturnBook_AllValid()
        {
            // Arrange
            string path = "../../../Data/books.json";
            JsonSerializerOptions options = new() { WriteIndented = true };
            string readerName = "Reader2";

            List<Book> bookList = new()
            {
                new Book("Name", "Author", "Category", "Language", DateTime.Now, "0001234567890"),
                new Book("Name2", "Author2", "Category2", "Language2", DateTime.Now, "0001234567893")
            };
            bookList[1].Reader = readerName;
            bookList[1].ReturnDate = DateTime.Today.AddDays(10);
            string jsonBookList = JsonSerializer.Serialize(bookList, options);
            File.WriteAllText(path, jsonBookList);

            bookList[1].Reader = null;
            bookList[1].ReturnDate = DateTime.MinValue;
            string jsonExpected = JsonSerializer.Serialize(bookList, options);

            // Act
            new Commander().ReturnBook(bookList[1].Name, readerName);
            string jsonActual = File.ReadAllText(path);
            File.Delete(path);

            // Assert
            Assert.Equal(jsonExpected, jsonActual);
        }

        [Fact]
        public void ReturnBook_ReaderHasntBorrowedAnyBooks()
        {
            // Arrange
            string path = "../../../Data/books.json";

            Book book1 = new("Name", "Author", "Category", "Language", DateTime.Now, "0001234567890");
            Book book2 = new("Name2", "Author2", "Category2", "Language2", DateTime.Now, "0001234567893");
            List<Book> bookList = new() { book1, book2 };
            bookList[0].Reader = "Reader1";
            string jsonBookList = JsonSerializer.Serialize(bookList, new() { WriteIndented = true });
            File.WriteAllText(path, jsonBookList);

            // Act
            bool readerValid = new ConstraintValidator().ReaderHasBooks("Reader2");
            File.Delete(path);

            // Assert
            Assert.False(readerValid);
        }
    }
}
