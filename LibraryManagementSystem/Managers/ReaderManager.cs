using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace LibraryManagementSystem.Managers
{
    public class ReaderManager
    {
        public List<Reader> Readers { get; private set; } = new List<Reader>();
        private const string FilePath = "readers.json";
        private int _nextId;

        public ReaderManager()
        {
            LoadFromFile();
            _nextId = Readers.Any() ? Readers.Max(r => int.Parse(r.Id)) + 1 : 1;
        }

        public void AddReader(Reader reader)
        {
            if (!Regex.IsMatch(reader.Name, @"^[А-Яа-яёЁ\s]+$"))
            {
                throw new ArgumentException("Имя читателя должно быть написано только русскими буквами.");
            }

            if (!Regex.IsMatch(reader.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                throw new ArgumentException("Email должен содержать только английские буквы и должен включать '@'.");
            }

            reader.Id = (_nextId++).ToString();
            Readers.Add(reader);
            SaveToFile();
        }

        public void RemoveReader(string readerId)
        {
            var reader = Readers.FirstOrDefault(r => r.Id == readerId);
            if (reader == null)
            {
                throw new KeyNotFoundException("Читатель с таким ID не найден.");
            }
            Readers.Remove(reader);
            SaveToFile();
        }

        public Reader GetReader(string readerId)
        {
            return Readers.FirstOrDefault(r => r.Id == readerId)
                   ?? throw new KeyNotFoundException("Читатель с таким ID не найден.");
        }

        public List<Reader> GetAllReaders() => Readers;

        private void SaveToFile()
        {
            var json = JsonConvert.SerializeObject(Readers, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }

        private void LoadFromFile()
        {
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                Readers = JsonConvert.DeserializeObject<List<Reader>>(json) ?? new List<Reader>();
            }
        }
    }
}