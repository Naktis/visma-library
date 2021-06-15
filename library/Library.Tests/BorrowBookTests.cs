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
            new Commander().BorrowBook(bookList[1].ISBN, bookList[1].Reader, bookList[1].ReturnDate);
            string jsonActual = File.ReadAllText(path);
            File.Delete(path);

            // Assert
            Assert.Equal(jsonExpected, jsonActual);
        }

        [Fact]
        public void BorrowBook_DisallowBorrowingMoreThan3Books()
        {
            // Arrange
            string path = "../../../Data/books.json";

            Book bookGeneric = new("Name", "Author", "Category", "Language", DateTime.Now, "0001234567890");
            Book bookToBorrow = new("Name2", "Author2", "Category2", "Language2", DateTime.Now, "0001234567893");
            List<Book> bookList = new() { bookGeneric, bookGeneric, bookGeneric, bookToBorrow };
            bookList[0].Reader = bookList[1].Reader = bookList[2].Reader = "Reader1";
            string jsonBookList = JsonSerializer.Serialize(bookList, new() { WriteIndented = true });
            File.WriteAllText(path, jsonBookList);

            // Act
            bool readerBlocked = new ConstraintValidator().BookLimitReached(bookList[0].Reader);
            File.Delete(path);

            // Assert
            Assert.True(readerBlocked);
        }

        [Fact]
        public void BorrowBook_CantBorrowNonexistantBook() // Conditions for deletion are the same
        {
            // Arrange
            string path = "../../../Data/books.json";

            Book book = new("Name", "Author", "Category", "Language", DateTime.Now, "0001234567890");
            List<Book> bookList = new() { book };
            string jsonBookList = JsonSerializer.Serialize(bookList, new() { WriteIndented = true });
            File.WriteAllText(path, jsonBookList);

            // Act
            List<Book> bookMatches = new ConstraintValidator().AvailableBooksByName("Name2");
            File.Delete(path);

            // Assert
            Assert.Empty(bookMatches);
        }

        [Fact]
        public void BorrowBook_CantBorrowAlreadyTakenBook() // Conditions for deletion are the same
        {
            // Arrange
            string path = "../../../Data/books.json";

            Book book = new("Name", "Author", "Category", "Language", DateTime.Now, "0001234567890");
            book.Reader = "Reader1";
            List<Book> bookList = new() { book };
            string jsonBookList = JsonSerializer.Serialize(bookList, new() { WriteIndented = true });
            File.WriteAllText(path, jsonBookList);

            // Act
            List<Book> bookMatches = new ConstraintValidator().AvailableBooksByName("Name");
            File.Delete(path);

            // Assert
            Assert.Empty(bookMatches);
        }
    }
}
