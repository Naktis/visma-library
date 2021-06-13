using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Book
    {
        public Book(string name, string author, string category, string language, DateTime publicationDate, string isbn)
        {
            Name = name;
            Author = author;
            Category = category;
            Language = language;
            PublicationDate = publicationDate;
            ISBN = isbn;
        }
        public string Name { get; }
        public string Author { get; }
        public string Category { get; }
        public string Language { get; }
        public DateTime PublicationDate { get; }
        public string ISBN { get; }
        public DateTime ReturnDate { get; set; }
        public string Reader { get; set; }
    }
}
