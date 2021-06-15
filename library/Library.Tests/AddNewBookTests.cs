using System;
using Xunit;
using Library;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Library.Tests
{
    public class AddNewBookTests
    {
        [Fact]
        public void Add_NewBook_ToEmptyLibrary()
        {
            // Arrange
            string path = "../../../Data/books.json";
            Book book = new("Name", "Author", "Category", "Language", DateTime.Now, "0001234567890");
            List<Book> booksExpected = new() { book };
            string jsonExpected = JsonSerializer.Serialize(booksExpected, new JsonSerializerOptions { WriteIndented = true });

            // Act
            new Commander().AddNewBook(book);
            string jsonActual = File.ReadAllText(path);
            File.Delete(path);

            // Assert
            Assert.Equal(jsonExpected, jsonActual);
        }

        [Fact]
        public void Add_NewBook_ToPopulatedLibrary()
        {
            // Arrange (until "Act")
            string path = "../../../Data/books.json";
            JsonSerializerOptions options = new() { WriteIndented = true };

            // Create a file with book information
            Book book = new("Name3", "Author3", "Category3", "Language3", DateTime.Today, "0001234567893");
            List<Book> libraryList = new()
            {
                new Book("Name1", "Author1", "Category1", "Language1", DateTime.Today, "0001234567891"),
                new Book("Name2", "Author2", "Category2", "Language2", DateTime.Today, "0001234567892")
            };
            string jsonLibraryList = JsonSerializer.Serialize(libraryList, options);
            File.WriteAllText(path, jsonLibraryList);

            // Form the expected final book list
            List<Book> booksExpected = new(libraryList);
            booksExpected.Add(book);
            string jsonExpected = JsonSerializer.Serialize(booksExpected, options);
            
            // Act
            new Commander().AddNewBook(book);
            string jsonActual = File.ReadAllText(path);
            File.Delete(path);

            // Assert
            Assert.Equal(jsonExpected, jsonActual);
        }
    }
}