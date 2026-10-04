using BookStore.Domain.Entities;

namespace BookStore.Application.Interfaces
{
    public interface IBookValidationService
    {
        Task ValidateAsync(Book book, CancellationToken ct = default);
    }
}