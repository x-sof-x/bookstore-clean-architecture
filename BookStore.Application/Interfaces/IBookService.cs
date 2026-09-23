using BookStore.Domain.Entities;
namespace BookStore.Application.Interfaces;

    public interface IBookService
    {
    IEnumerable<Book> GetAll(string? author = null, int? publishedYear = null);
    Book? GetById(int id);
   bool Add(Book book);
    bool Delete(int id);
    bool Update(Book book);

}

