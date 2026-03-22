namespace Domain
{
    public class Clothing
    {
        public int Id { get; set; }
        public int UserId { get; set; }  
        public string? Category { get; set; }
        public string? Color { get; set; }
        public string ImageUrl { get; set; }
        public string? Season { get; set; }
        public string? Style { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
    }
}