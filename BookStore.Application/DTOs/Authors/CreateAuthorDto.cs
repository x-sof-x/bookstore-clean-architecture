using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.DTOs.Authors;

public class CreateAuthorDto
{
    [Required(ErrorMessage = "Ім'я автора обов'язкове.")]
    [StringLength(150, ErrorMessage = "Ім'я автора не може бути довшим за 150 символів.")]
    public string Name { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "Біографія не може бути довшою за 1000 символів.")]
    public string Biography { get; set; } = string.Empty;
}