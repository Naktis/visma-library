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
        private readonly FileManipulator IO; // File input/output

        public ConstraintValidator()
        {
            IO = new();
        }

        // This method is called before allowing user to borrow books
        // Returns true, if the reader has already borrowed 3 books
        public bool BookLimitReached(string readerName)
        {
            List<Book> books = IO.GetBookList();

            int borrowedBooks = books.Count(x => x.Reader == readerName);

            if (borrowedBooks < 3)
                return false;
            else 
                return true;
        }

        // This method is called when it's wanted to borrow or delete a book to
        //      check if it's eligible for the action
        // Returns all copies of books that share the same name and aren't borrowed
        public List<Book> AvailableBooksByName(string bookName)
        {
            List<Book> books = IO.GetBookList();
            return books.FindAll(x => x.Name == bookName && x.Reader == null);
        }

        // This method is called before allowing reader to return anything
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
