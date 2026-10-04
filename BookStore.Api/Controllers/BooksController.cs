using Microsoft.AspNetCore.Mvc;
using BookStore.Application.Interfaces;
using BookStore.Application.DTOs.Books;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: /books?author=...&publishedYear=...
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? author, [FromQuery] int? publishedYear, CancellationToken ct)
        {
            var books = await _bookService.GetAllAsync(author, publishedYear, ct);
            return Ok(books);
        }

        // GET: /books/{id}
        [HttpGet("{id:guid}", Name = "GetBookById")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var book = await _bookService.GetByIdAsync(id, ct);
            return Ok(book);
        }

        // POST: /books
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookDto dto, CancellationToken ct)
        {
            var createdBook = await _bookService.AddAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = createdBook.Id }, createdBook);
        }

        // PUT: /books/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBookDto dto, CancellationToken ct)
        {
            await _bookService.UpdateAsync(id, dto, ct);
            return NoContent();
        }

        // DELETE: /books/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            await _bookService.DeleteAsync(id, ct);
            return NoContent();
        }
    }
}