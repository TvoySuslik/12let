using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace LibraryManagementSystem.Managers
{
    public class BookManager
    {
        public List<Book> Books { get; private set; } = new List<Book>();
        private const string FilePath = "books.json";
        private int _nextId;

        public BookManager()
        {
            LoadFromFile();
            _nextId = Books.Any() ? Books.Max(b => int.Parse(b.Id)) + 1 : 1;  // Set the next ID based on existing books
        }

        public void AddBook(Book book)
        {
            if (!Regex.IsMatch(book.Author, @"^[А-Яа-яёЁ\s]+$"))
            {
                throw new ArgumentException("Имя автора должно быть написано только русскими буквами.");
            }

            if (book.YearPublished < 1 || book.YearPublished > 2024)
            {
                throw new ArgumentException("Год издания должен быть в пределах от 1 до 2024.");
            }

            book.Id = (_nextId++).ToString();
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