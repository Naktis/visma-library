using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Commander
    {
        private readonly FileManipulator IO; // input/output

        public Commander()
        {
            IO = new();
        }

        public void AddNewBook(Book newBook)
        {
            List<Book> books = IO.GetBookList();
            books.Add(newBook);
            IO.SaveBookList(books);
        }

        public void BorrowBook(string isbn, string readerName, DateTime returnDate)
        {
            List<Book> books = IO.GetBookList();
            Book bookMatch = books.FirstOrDefault(x => x.ISBN == isbn);

            if (bookMatch != null)
            {
                bookMatch.Reader = readerName;
                bookMatch.ReturnDate = returnDate;
            }

            IO.SaveBookList(books);
        }

        public void ReturnBook(string bookName, string readerName)
        {
            List<Book> books = IO.GetBookList();
            Book bookMatch = books.FirstOrDefault(x => x.Reader == readerName && x.Name == bookName);

            if (bookMatch != null)
            {
                bookMatch.Reader = null;
                bookMatch.ReturnDate = DateTime.MinValue;
            }
                
            IO.SaveBookList(books);
        }

        public List<Book> ListBooks(int option, string label)
        {
            List<Book> books = IO.GetBookList();
            List<Book> filteredBooks = option switch
            {
                2 => books.FindAll(x => x.Author == label),
                3 => books.FindAll(x => x.Category == label),
                4 => books.FindAll(x => x.Language == label),
                5 => books.FindAll(x => x.ISBN == label),
                6 => books.FindAll(x => x.Name == label),
                7 => books.FindAll(x => x.Reader == null), // Available
                8 => books.FindAll(x => x.Reader != null), // Taken
                9 => books.FindAll(x => x.Reader == label),
                _ => books,
            };
            return filteredBooks;
        }

        public void DeleteBook(string isbn) {
            List<Book> books = IO.GetBookList();
            Book bookMatch = books.FirstOrDefault(x => x.ISBN == isbn);
            books.Remove(bookMatch);
            IO.SaveBookList(books);
        }
    }
}
