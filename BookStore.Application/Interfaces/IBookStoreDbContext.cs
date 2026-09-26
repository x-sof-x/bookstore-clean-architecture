using Microsoft.EntityFrameworkCore;
using BookStore.Domain.Entities;
using System.Collections.Generic;

namespace BookStore.Application.Interfaces
{
    public interface IBookStoreDbContext
    {
        DbSet<Book> Books { get; }
        DbSet<Author> Authors { get; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
