using Microsoft.AspNetCore.Mvc;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        public IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet]
        public IActionResult GetAll([FromQuery] string? author, [FromQuery] int? publishedYear)
        {
            return Ok(_bookService.GetAll( author, publishedYear));

        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _bookService.GetById(id);
            if (user == null)
            {
                return NotFound($" нигу з Id {id} не знайдено.");
            }
            return Ok(user);
        }
        [HttpPost]
        public IActionResult AddUser([FromBody] Book book)
        {
            _bookService.Add(book);
            return Ok(book);
        }
        [HttpPut("{id}")]
        public IActionResult Update( int id, [FromBody] Book book)
        {
            book.Id = id;
            var isUpdated = _bookService.Update(book);
            if (!isUpdated)
            {
                return NotFound($" нигу з Id {id} не знайдено.");
            }
            return Ok(book);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete([FromBody] int id)
        {
            var isDeleted = _bookService.Delete(id);
            if (!isDeleted)
            {
                return NotFound($" нигу з Id {id} не знайдено.");
            }

            return NoContent();
        }
}
}
