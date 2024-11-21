namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int YearPublished { get; set; }

        public override string ToString()
        {
            return $"[ID: {Id}] {Title} by {Author}, {YearPublished}";
        }
    }
}