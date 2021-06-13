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
    public class BorrowBookTests
    {
        [Fact]
        public void BorrowBook_AllValid()
        {
            // Arrange
            Service service = new();
            string path = "../../../Data/books.json";
            JsonSerializerOptions options = new() { WriteIndented = true };

            List<Book> bookList = new()
            {
                new Book("Name", "Author", "Category", "Language", DateTime.Now, "0001234567890"),
                new Book("Name2", "Author2", "Category2", "Language2", DateTime.Now, "0001234567893")
            };
            string jsonBookList = JsonSerializer.Serialize(bookList, options);
            File.WriteAllText(path, jsonBookList);

            bookList[1].Reader = "Reader1";
            bookList[1].ReturnDate = DateTime.Today.AddDays(10);
            string jsonExpected = JsonSerializer.Serialize(bookList, options);

            // Act
            service.BorrowBook(bookList[1].Name, bookList[1].Reader, bookList[1].ReturnDate);
            string jsonActual = File.ReadAllText(path);
            File.Delete(path);

            // Assert
            Assert.Equal(jsonExpected, jsonActual);
        }

        [Fact]
        public void BorrowBook_DisallowBorrowingMoreThan3Books()
        {
            // Arrange
            Service service = new();
            string path = "../../../Data/books.json";
            JsonSerializerOptions options = new() { WriteIndented = true };

            Book bookGeneric = new("Name", "Author", "Category", "Language", DateTime.Now, "0001234567890");
            Book bookToBorrow = new("Name2", "Author2", "Category2", "Language2", DateTime.Now, "0001234567893");
            List<Book> bookList = new() { bookGeneric, bookGeneric, bookGeneric, bookToBorrow };
            bookList[0].Reader = bookList[1].Reader = bookList[2].Reader = "Reader1";
            string jsonBookList = JsonSerializer.Serialize(bookList, options);
            File.WriteAllText(path, jsonBookList);

            // Act
            bool readerBlocked = service.BookLimitReached(bookList[0].Reader);
            File.Delete(path);

            // Assert
            Assert.True(readerBlocked);
        }

        [Fact]
        public void BorrowBook_CantBorrowNonexistantBook()
        {
            // Arrange
            Service service = new();
            string path = "../../../Data/books.json";
            JsonSerializerOptions options = new() { WriteIndented = true };

            Book book = new("Name", "Author", "Category", "Language", DateTime.Now, "0001234567890");
            List<Book> bookList = new() { book };
            string jsonBookList = JsonSerializer.Serialize(bookList, options);
            File.WriteAllText(path, jsonBookList);

            // Act
            bool bookExists = service.BookExistsAvailable("Name2");
            File.Delete(path);

            // Assert
            Assert.False(bookExists);
        }

        [Fact]
        public void BorrowBook_CantBorrowAlreadyTakenBook()
        {
            // Arrange
            Service service = new();
            string path = "../../../Data/books.json";
            JsonSerializerOptions options = new() { WriteIndented = true };

            Book book = new("Name", "Author", "Category", "Language", DateTime.Now, "0001234567890");
            book.Reader = "Reader1";
            List<Book> bookList = new() { book };
            string jsonBookList = JsonSerializer.Serialize(bookList, options);
            File.WriteAllText(path, jsonBookList);

            // Act
            bool bookAvailable = service.BookExistsAvailable("Name");
            File.Delete(path);

            // Assert
            Assert.False(bookAvailable);
        }
    }
}
