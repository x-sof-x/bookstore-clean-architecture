using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.DTOs.Books;

public class UpdateBookDto
{
    [Required(ErrorMessage = "Назва книги обов'язкова.")]
    [StringLength(30, ErrorMessage = "Назва книги не може бути довшою за 30 символів.")]
    public string Title { get; set; } = string.Empty;

    [Range(0, 100000, ErrorMessage = "Ціна має бути від 0 до 100000.")]
    public decimal Price { get; set; }

    [Range(1, 2100, ErrorMessage = "Некоректний рік видання.")]
    public int PublishedYear { get; set; }

    public Guid AuthorId { get; set; }
}