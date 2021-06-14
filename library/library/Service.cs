using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace Library
{
    public class Service
    {
        private readonly string _path = "../../../Data/books.json";

        public void AddNewBook(Book newBook)
        {
            List<Book> books = GetBookList();
            books.Add(newBook);
            SaveBookList(books);
        }

        public void BorrowBook(string bookName, string readerName, DateTime returnDate)
        {
            List<Book> books = GetBookList();

            Book book = books.FirstOrDefault(x => x.Name == bookName);

            if (book != null)
            {
                book.Reader = readerName;
                book.ReturnDate = returnDate;
            }

            SaveBookList(books);
        }

        public bool BookLimitReached(string readerName)
        {
            List<Book> books = GetBookList();

            int borrowedBooks = books.Count(x => x.Reader == readerName);

            if (borrowedBooks < 3)
                return false;
            else 
                return true;
        }

        public bool BookExistsAvailable(string bookName)
        {
            List<Book> books = GetBookList();

            Book bookMatch = books.FirstOrDefault(x => x.Name == bookName);

            if (bookMatch != null && bookMatch.Reader == null)
                return true;
            else
                return false;
        }

        public bool ReaderHasBooks(string readerName)
        {
            List<Book> books = GetBookList();

            Book bookMatch = books.FirstOrDefault(x => x.Reader == readerName);

            if (bookMatch != null)
                return true;
            else
                return false;
        }

        public List<Book> GetReaderBooks(string readerName)
        {
            List<Book> books = GetBookList();

            return books.FindAll(x => x.Reader == readerName);
        }

        public void ReturnBook(string bookName, string readerName)
        {
            List<Book> books = GetBookList();
            Book bookMatch = books.FirstOrDefault(x => x.Reader == readerName && x.Name == bookName);

            bookMatch.Reader = null;
            bookMatch.ReturnDate = DateTime.MinValue;

            SaveBookList(books);
        }

        private List<Book> GetBookList()
        {
            List<Book> books = new();
            string jsonData;

            if (File.Exists(_path))
            {
                jsonData = File.ReadAllText(_path);
                books = JsonSerializer.Deserialize<List<Book>>(jsonData);
            }
            return books;
        }

        private void SaveBookList(List<Book> books)
        {
            JsonSerializerOptions options = new() { WriteIndented = true };
            string jsonData = JsonSerializer.Serialize(books, options);
            File.WriteAllText(_path, jsonData);
        }
    }
}
