namespace LibraryManagementSystem.Dtos
{
    public class AddBookDto
    {
        public string BookName { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int TotalCopies { get; set; }
        public List<int> CategoryIds { get; set; } = new();
    }
}
