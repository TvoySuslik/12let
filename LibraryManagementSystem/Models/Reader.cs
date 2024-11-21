namespace LibraryManagementSystem.Models
{
    public class Reader
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return $"[ID: {Id}] {Name}, Email: {Email}";
        }
    }
}