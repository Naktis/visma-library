using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Library
{
    class FileManipulator
    {
        private readonly string _path = "../../../Data/books.json";
        public List<Book> GetBookList()
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

        public void SaveBookList(List<Book> books)
        {
            JsonSerializerOptions options = new() { WriteIndented = true };
            string jsonData = JsonSerializer.Serialize(books, options);
            File.WriteAllText(_path, jsonData);
        }
    }
}
