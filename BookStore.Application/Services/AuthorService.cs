namespace BookStore.Application.Interfaces
{
    public class AuthorService: IAuthorService
    {
        private readonly List <string> _authors = new List<string>
            {
            "George Orwell",
            "J.R.R. Tolkien",
            "Тарас Шевченко",
            "Frank Herbert",
            "Михайло Коцюбинський"
        };
        public bool AuthorExists(string author)
        {
            return _authors.Any(a => a.Equals(author, StringComparison.OrdinalIgnoreCase));
        }
        public void Add(string author)
        {
            if (!AuthorExists(author))
            {
                _authors.Add(author);
            }
        }
        public void Delete(string author)
        {
            var existingAuthor = _authors.FirstOrDefault(a => a.Equals(author, StringComparison.OrdinalIgnoreCase));
            if (existingAuthor != null)
            {
                _authors.Remove(existingAuthor);
            }
        }
    }
}
