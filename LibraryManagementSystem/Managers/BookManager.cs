using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace LibraryManagementSystem.Managers
{
    public class BookManager
    {
        public List<Book> Books { get; private set; } = new List<Book>();
        private const string FilePath = "books.json";

        public BookManager()
        {
            LoadFromFile();
        }

        public void AddBook(Book book)
        {
            if (Books.Any(b => b.Id == book.Id))
            {
                throw new InvalidOperationException("Книга с таким ID уже существует.");
            }
            Books.Add(book);
            SaveToFile();
        }

        public void RemoveBook(string bookId)
        {
            var book = Books.FirstOrDefault(b => b.Id == bookId);
            if (book == null)
            {
                throw new KeyNotFoundException("Книга с таким ID не найдена.");
            }
            Books.Remove(book);
            SaveToFile();
        }

        public Book GetBook(string bookId)
        {
            return Books.FirstOrDefault(b => b.Id == bookId)
                   ?? throw new KeyNotFoundException("Книга с таким ID не найдена.");
        }

        public List<Book> GetAllBooks() => Books;

        private void SaveToFile()
        {
            var json = JsonConvert.SerializeObject(Books, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }

        private void LoadFromFile()
        {
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                Books = JsonConvert.DeserializeObject<List<Book>>(json) ?? new List<Book>();
            }
        }
    }
}