using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace LibraryManagementSystem.Managers
{
    public class ReaderManager
    {
        public List<Reader> Readers { get; private set; } = new List<Reader>();
        private const string FilePath = "readers.json";

        public ReaderManager()
        {
            LoadFromFile();
        }

        public void AddReader(Reader reader)
        {
            if (Readers.Any(r => r.Id == reader.Id))
            {
                throw new InvalidOperationException("Читатель с таким ID уже существует.");
            }
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