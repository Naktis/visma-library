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

        public void BorrowBook(string bookName, string readerName, DateTime returnDate)
        {
            List<Book> books = IO.GetBookList();

            Book book = books.FirstOrDefault(x => x.Name == bookName);

            if (book != null)
            {
                book.Reader = readerName;
                book.ReturnDate = returnDate;
            }

            IO.SaveBookList(books);
        }

        public void ReturnBook(string bookName, string readerName)
        {
            List<Book> books = IO.GetBookList();
            Book bookMatch = books.FirstOrDefault(x => x.Reader == readerName && x.Name == bookName);

            bookMatch.Reader = null;
            bookMatch.ReturnDate = DateTime.MinValue;

            IO.SaveBookList(books);
        }
    }
}
