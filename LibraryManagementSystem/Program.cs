using LibraryManagementSystem.Managers;
using LibraryManagementSystem.Models;
using System;

namespace LibraryManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var bookManager = new BookManager();
            var readerManager = new ReaderManager();

            while (true)
            {
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1 - Добавить книгу");
                Console.WriteLine("2 - Удалить книгу");
                Console.WriteLine("3 - Просмотреть книгу");
                Console.WriteLine("4 - Список всех книг");
                Console.WriteLine("5 - Добавить читателя");
                Console.WriteLine("6 - Удалить читателя");
                Console.WriteLine("7 - Просмотреть читателя");
                Console.WriteLine("8 - Список всех читателей");
                Console.WriteLine("9 - Выйти");

                var choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            Console.Write("Введите ID книги: ");
                            var bookId = Console.ReadLine();
                            Console.Write("Введите название книги: ");
                            var title = Console.ReadLine();
                            Console.Write("Введите автора книги: ");
                            var author = Console.ReadLine();
                            Console.Write("Введите год издания книги: ");
                            var year = int.Parse(Console.ReadLine());

                            bookManager.AddBook(new Book { Id = bookId, Title = title, Author = author, YearPublished = year });
                            Console.WriteLine("Книга добавлена.");
                            break;

                        case "2":
                            Console.Write("Введите ID книги для удаления: ");
                            bookId = Console.ReadLine();
                            bookManager.RemoveBook(bookId);
                            Console.WriteLine("Книга удалена.");
                            break;

                        case "3":
                            Console.Write("Введите ID книги для просмотра: ");
                            bookId = Console.ReadLine();
                            Console.WriteLine(bookManager.GetBook(bookId));
                            break;

                        case "4":
                            Console.WriteLine("Список всех книг:");
                            foreach (var book in bookManager.GetAllBooks())
                            {
                                Console.WriteLine(book);
                            }
                            break;

                        case "5":
                            Console.Write("Введите ID читателя: ");
                            var readerId = Console.ReadLine();
                            Console.Write("Введите имя читателя: ");
                            var name = Console.ReadLine();
                            Console.Write("Введите email читателя: ");
                            var email = Console.ReadLine();

                            readerManager.AddReader(new Reader { Id = readerId, Name = name, Email = email });
                            Console.WriteLine("Читатель добавлен.");
                            break;

                        case "6":
                            Console.Write("Введите ID читателя для удаления: ");
                            readerId = Console.ReadLine();
                            readerManager.RemoveReader(readerId);
                            Console.WriteLine("Читатель удален.");
                            break;
                        case "7":
                            Console.Write("Введите ID читателя для просмотра: ");
                            readerId = Console.ReadLine();
                            Console.WriteLine(readerManager.GetReader(readerId));
                            break;

                        case "8":
                            Console.WriteLine("Список всех читателей:");
                            foreach (var reader in readerManager.GetAllReaders())
                            {
                                Console.WriteLine(reader);
                            }
                            break;

                        case "9":
                            return;

                        default:
                            Console.WriteLine("Неверный выбор.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }
    }
}