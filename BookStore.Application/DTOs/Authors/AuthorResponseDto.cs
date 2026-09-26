namespace BookStore.Application.DTOs.Authors;

public class AuthorResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Biography { get; set; } = string.Empty;
    public List<string> BookTitles { get; set; } = new();
}