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
    class Service
    {
        public void AddNewBook(Book newBook)
        {
            string path = "../../../Data/books.json";

            List<Book> books = new List<Book>();
            string jsonData;

            if (File.Exists(path))
            { 
                jsonData = File.ReadAllText(path);
                books = JsonSerializer.Deserialize<List<Book>>(jsonData);
            }

            books.Add(newBook);

            JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
            jsonData = JsonSerializer.Serialize(books, options);
            File.WriteAllText(path, jsonData);
        }
    }
}
