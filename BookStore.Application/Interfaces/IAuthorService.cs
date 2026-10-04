using BookStore.Application.DTOs.Authors;

namespace BookStore.Application.Interfaces;

public interface IAuthorService
{
    Task<IEnumerable<AuthorResponseDto>> GetAllAsync(CancellationToken ct = default);
    Task<AuthorResponseDto> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<AuthorResponseDto> AddAsync(CreateAuthorDto dto, CancellationToken ct = default);
    Task UpdateAsync(Guid id, UpdateAuthorDto dto, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
    Task<bool> AuthorExistsAsync(Guid authorId, CancellationToken ct = default);
}