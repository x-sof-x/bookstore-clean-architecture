using BookStore.Domain.Entities;

namespace BookStore.Application.Interfaces
{
    public interface IBookValidationService
    {
        void Validate(Book book);
    }
}