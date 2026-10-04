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
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var authors = await _authorService.GetAllAsync(ct);
            return Ok(authors);
        }

        // GET: /Authors/{id}
        [HttpGet("{id:guid}", Name = "GetAuthorById")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var author = await _authorService.GetByIdAsync(id, ct);
            return Ok(author);
        }

        // POST: /Authors
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAuthorDto dto, CancellationToken ct)
        {
            var createdAuthor = await _authorService.AddAsync(dto, ct);
            return CreatedAtAction("GetAuthorById", new { id = createdAuthor.Id }, createdAuthor);
        }

        // PUT: /Authors/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAuthorDto dto, CancellationToken ct)
        {
            await _authorService.UpdateAsync(id, dto, ct);
            return NoContent();
        }

        // DELETE: /Authors/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            await _authorService.DeleteAsync(id, ct);
            return NoContent();
        }
    }
}