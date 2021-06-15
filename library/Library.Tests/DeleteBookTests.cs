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
    public class DeleteBookTests
    {
        [Fact]
        public void DeleteBook_AllValid()
        {
            // Arrange
            string path = "../../../Data/books.json";
            JsonSerializerOptions options = new() { WriteIndented = true };
            Book bookToRemove = new("Name2", "Author2", "Category2", "Language2", DateTime.Now, "0001234567893");

            List<Book> bookList = new()
            {
                new Book("Name", "Author", "Category", "Language", DateTime.Now, "0001234567890"),
                bookToRemove
            };
            string jsonBookList = JsonSerializer.Serialize(bookList, options);
            File.WriteAllText(path, jsonBookList);

            bookList.RemoveAt(1);
            string jsonExpected = JsonSerializer.Serialize(bookList, options);

            // Act
            new Commander().DeleteBook(bookToRemove.ISBN);
            string jsonActual = File.ReadAllText(path);
            File.Delete(path);

            // Assert
            Assert.Equal(jsonExpected, jsonActual);
        }

        // Tests for deleting non existant or taken books match tests for borrowing books
        // which can be found at BorrowBookTests.cs
    }
}
