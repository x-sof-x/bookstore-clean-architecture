using BookStore.Domain.Entities;
namespace BookStore.Application.Interfaces
{
    public interface IBookValidationService
    {
        bool AddBookValidation(Book book);
    }
}
