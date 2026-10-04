using Microsoft.EntityFrameworkCore;
using BookStore.Domain.Entities;

namespace BookStore.Application.Interfaces
{
    public interface IBookStoreDbContext
    {
        DbSet<Book> Books { get; }
        DbSet<Author> Authors { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
