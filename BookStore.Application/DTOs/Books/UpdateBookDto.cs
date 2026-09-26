namespace BookStore.Application.DTOs.Books;

public class UpdateBookDto
{
    public string Title { get; set; } = string.Empty;
    
    public decimal Price { get; set; }
    public int PublishedYear { get; set; }
    public Guid AuthorId { get; set; }
   
}