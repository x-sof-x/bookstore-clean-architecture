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
        public IActionResult GetAll([FromQuery] string? author, [FromQuery] int? publishedYear)
        {
            var books = _bookService.GetAll(author, publishedYear);
            return Ok(books);
        }

        // GET: /books/{id}
        [HttpGet("{id:guid}", Name = "GetBookById")]
        public IActionResult GetById(Guid id)
        {
            var book = _bookService.GetById(id);
            return Ok(book);
        }

        // POST: /books
        [HttpPost]
        public IActionResult Create([FromBody] CreateBookDto dto)
        {
            var createdBook = _bookService.Add(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdBook.Id }, createdBook);
        }

        // PUT: /books/{id}
        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] UpdateBookDto dto)
        {
            _bookService.Update(id, dto);
            return NoContent();
        }

        // DELETE: /books/{id}
        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            _bookService.Delete(id);
            return NoContent();
        }
    }
}