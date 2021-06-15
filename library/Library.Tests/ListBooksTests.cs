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
    public class ListBooksTests
    {
        [Fact]
        public void ListBooks_All()
        {
            // Arrange
            string path = "../../../Data/books.json";
            JsonSerializerOptions options = new() { WriteIndented = true };

            List<Book> books = GetBaseList();
            string jsonBookList = JsonSerializer.Serialize(books, options);
            File.WriteAllText(path, jsonBookList);
            string jsonExpected = jsonBookList;

            // Act
            string jsonActual = JsonSerializer.Serialize(new Commander().ListBooks(1, ""), options);
            File.Delete(path);

            // Assert
            Assert.Equal(jsonExpected, jsonActual);
        }

        [Fact]
        public void ListBooks_FilterByAuthor()
        {
            // Arrange
            JsonSerializerOptions options = new() { WriteIndented = true };

            WriteBaseFile();
            string jsonExpected = JsonSerializer.Serialize(new List<Book>()
            {
                new Book("Name1", "Same author", "Category3", "Language3", DateTime.MinValue.AddYears(2003), "0001234567893"),
                new Book("Name2", "Same author", "Category4", "Language4", DateTime.MinValue.AddYears(2004), "0001234567894")
            }, options);

            // Act
            string jsonActual = JsonSerializer.Serialize(new Commander().ListBooks(2, "Same author"), options);
            File.Delete("../../../Data/books.json");

            // Assert
            Assert.Equal(jsonExpected, jsonActual);
        }

        [Fact]
        public void ListBooks_FilterByCategory()
        {
            // Arrange
            JsonSerializerOptions options = new() { WriteIndented = true };

            WriteBaseFile();
            string jsonExpected = JsonSerializer.Serialize(new List<Book>()
            {
                new Book("Name3", "Author3", "Same category", "Language5", DateTime.MinValue.AddYears(2005), "0001234567895"),
                new Book("Name4", "Author4", "Same category", "Language6", DateTime.MinValue.AddYears(2006), "0001234567896")
            }, options);

            // Act
            string jsonActual = JsonSerializer.Serialize(new Commander().ListBooks(3, "Same category"), options);
            File.Delete("../../../Data/books.json");

            // Assert
            Assert.Equal(jsonExpected, jsonActual);
        }

        [Fact]
        public void ListBooks_FilterByLanguage()
        {
            // Arrange
            JsonSerializerOptions options = new() { WriteIndented = true };

            WriteBaseFile();
            string jsonExpected = JsonSerializer.Serialize(new List<Book>()
            {
                new Book("Name5", "Author5", "Category5", "Same language", DateTime.MinValue.AddYears(2007), "0001234567897"),
                new Book("Name6", "Author6", "Category6", "Same language", DateTime.MinValue.AddYears(2008), "0001234567898")
            }, options);

            // Act
            string jsonActual = JsonSerializer.Serialize(new Commander().ListBooks(4, "Same language"), options);
            File.Delete("../../../Data/books.json");

            // Assert
            Assert.Equal(jsonExpected, jsonActual);
        }

        [Fact]
        public void ListBooks_FilterByISBN()
        {
            // Arrange
            JsonSerializerOptions options = new() { WriteIndented = true };

            WriteBaseFile();
            string jsonExpected = JsonSerializer.Serialize(new List<Book>()
            {
                new Book("Name7", "Author7", "Category7", "Language7", DateTime.MinValue.AddYears(2009), "0001234567899"),
                new Book("Name8", "Author8", "Category8", "Language8", DateTime.MinValue.AddYears(2010), "0001234567899")
            }, options);

            // Act
            string jsonActual = JsonSerializer.Serialize(new Commander().ListBooks(5, "0001234567899"), options);
            File.Delete("../../../Data/books.json");

            // Assert
            Assert.Equal(jsonExpected, jsonActual);
        }

        [Theory]
        [InlineData(6, "Same name")]
        [InlineData(8, "")]
        public void ListBooks_FilterByNameAndTaken(int option, string label)
        {
            // Arrange
            JsonSerializerOptions options = new() { WriteIndented = true };

            WriteBaseFile();
            List<Book> books = new()
            {
                new Book("Same name", "Author1", "Category1", "Language1", DateTime.MinValue.AddYears(2001), "0001234567891"),
                new Book("Same name", "Author2", "Category2", "Language2", DateTime.MinValue.AddYears(2002), "0001234567892")
            };

            books[0].Reader = books[1].Reader = "Reader1";
            string jsonExpected = JsonSerializer.Serialize(books, options);

            // Act
            string jsonActual = JsonSerializer.Serialize(new Commander().ListBooks(option, label), options);
            File.Delete("../../../Data/books.json");

            // Assert
            Assert.Equal(jsonExpected, jsonActual);
        }

        private List<Book> GetBaseList()
        {
            List<Book> bookList = new()
            {
                new Book("Same name", "Author1", "Category1", "Language1", DateTime.MinValue.AddYears(2001), "0001234567891"),
                new Book("Same name", "Author2", "Category2", "Language2", DateTime.MinValue.AddYears(2002), "0001234567892"),
                new Book("Name1", "Same author", "Category3", "Language3", DateTime.MinValue.AddYears(2003), "0001234567893"),
                new Book("Name2", "Same author", "Category4", "Language4", DateTime.MinValue.AddYears(2004), "0001234567894"),
                new Book("Name3", "Author3", "Same category", "Language5", DateTime.MinValue.AddYears(2005), "0001234567895"),
                new Book("Name4", "Author4", "Same category", "Language6", DateTime.MinValue.AddYears(2006), "0001234567896"),
                new Book("Name5", "Author5", "Category5", "Same language", DateTime.MinValue.AddYears(2007), "0001234567897"),
                new Book("Name6", "Author6", "Category6", "Same language", DateTime.MinValue.AddYears(2008), "0001234567898"),
                new Book("Name7", "Author7", "Category7", "Language7", DateTime.MinValue.AddYears(2009), "0001234567899"),
                new Book("Name8", "Author8", "Category8", "Language8", DateTime.MinValue.AddYears(2010), "0001234567899"),
                new Book("Name9", "Author9", "Category9", "Language9", DateTime.MinValue.AddYears(2011), "0001234567900"),
                new Book("Name10", "Author10", "Category10", "Language10", DateTime.MinValue.AddYears(2012), "0001234567901"),
            };

            bookList[0].Reader = bookList[1].Reader = "Reader1";

            return bookList;
        }
        private void WriteBaseFile()
        {
            string json = JsonSerializer.Serialize(GetBaseList(), new() { WriteIndented = true });
            File.WriteAllText("../../../Data/books.json", json);
        }
    }
}
