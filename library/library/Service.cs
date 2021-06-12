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
        public void AddNewBook(Book newBook)
        {
            List<Book> books = new();
            string jsonData;
            string path = "../../../Data/books.json";

            if (File.Exists(path))
            { 
                jsonData = File.ReadAllText(path);
                books = JsonSerializer.Deserialize<List<Book>>(jsonData);
            }

            books.Add(newBook);

            JsonSerializerOptions options = new() { WriteIndented = true };
            jsonData = JsonSerializer.Serialize(books, options);
            File.WriteAllText(path, jsonData);
        }
    }
}
