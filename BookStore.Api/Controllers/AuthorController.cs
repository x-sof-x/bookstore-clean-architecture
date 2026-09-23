using Microsoft.AspNetCore.Mvc;
using BookStore.Application.Interfaces;
namespace WebApplication2.Controllers;
[ApiController]
[Route("[controller]")]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _authorService;
    public AuthorController(IAuthorService authorService)
    {
        _authorService = authorService;
    }
    [HttpPost]
    public IActionResult AddAuthor([FromBody] string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest("Ім'я автора не може бути порожнім.");

        _authorService.Add(name);
        return Ok($"Автора '{name}' успішно додано.");
    }
    [HttpDelete("{name}")]
    public IActionResult DeleteAuthor(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest("Ім'я автора не може бути порожнім.");
        _authorService.Delete(name);
        return Ok($"Автора '{name}' успішно видалено.");
    }
}
