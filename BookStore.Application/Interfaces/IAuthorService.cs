using BookStore.Application.DTOs.Authors;

namespace BookStore.Application.Interfaces;

public interface IAuthorService
{
    IEnumerable<AuthorResponseDto> GetAll();
    AuthorResponseDto GetById(Guid id);
    AuthorResponseDto Add(CreateAuthorDto dto);
    void Update(Guid id, UpdateAuthorDto dto);
    void Delete(Guid id);
    bool AuthorExists(Guid authorId);
}