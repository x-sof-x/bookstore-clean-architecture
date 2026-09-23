namespace BookStore.Application.Interfaces
{
    public interface IAuthorService
    {
        bool AuthorExists(string author);
        void Add(string author);
        void Delete(string author);
    }
}
