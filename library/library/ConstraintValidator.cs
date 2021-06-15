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
    public class ConstraintValidator
    {
        private readonly FileManipulator IO; // input/output

        public ConstraintValidator()
        {
            IO = new();
        }

        public bool BookLimitReached(string readerName)
        {
            List<Book> books = IO.GetBookList();

            int borrowedBooks = books.Count(x => x.Reader == readerName);

            if (borrowedBooks < 3)
                return false;
            else 
                return true;
        }

        public List<Book> AvailableBooksByName(string bookName)
        {
            List<Book> books = IO.GetBookList();
            return books.FindAll(x => x.Name == bookName && x.Reader == null);
        }

        public bool ReaderHasBooks(string readerName)
        {
            List<Book> books = IO.GetBookList();

            Book bookMatch = books.FirstOrDefault(x => x.Reader == readerName);

            if (bookMatch != null)
                return true;
            else
                return false;
        }
    }
}
