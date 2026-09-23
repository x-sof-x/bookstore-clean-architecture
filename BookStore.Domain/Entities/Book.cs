namespace BookStore.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Author { get; set; } = null!;

        public int PublishedYear { get; set; }
    }
}
