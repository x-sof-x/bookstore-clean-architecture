using BookStore.Domain.Entities;

namespace BookStore.Application
{
    public class DefaultBooksOptions
    {
        public const string SectionName = "DefaultBooks";

        public List<Book> Books { get; set; } = new();
    }
}