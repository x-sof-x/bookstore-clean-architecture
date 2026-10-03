using Microsoft.AspNetCore.Mvc;
using BookStore.Application.Interfaces;
using BookStore.Application.DTOs.Authors;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // GET: /Authors
        [HttpGet]
        public IActionResult GetAll()
        {
            var authors = _authorService.GetAll();
            return Ok(authors);
        }

        // GET: /Authors/{id}
        [HttpGet("{id:guid}", Name = "GetAuthorById")]
        public IActionResult GetById(Guid id)
        {
            var author = _authorService.GetById(id);
            return Ok(author);
        }

        // POST: /Authors
        [HttpPost]
        public IActionResult Create([FromBody] CreateAuthorDto dto)
        {
            var createdAuthor = _authorService.Add(dto);
            return CreatedAtAction("GetAuthorById", new { id = createdAuthor.Id }, createdAuthor);
        }

        // PUT: /Authors/{id}
        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] UpdateAuthorDto dto)
        {
            _authorService.Update(id, dto);
            return NoContent();
        }

        // DELETE: /Authors/{id}
        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            _authorService.Delete(id);
            return NoContent();
        }
    }
}