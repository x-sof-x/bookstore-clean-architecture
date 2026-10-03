namespace BookStore.Domain.Exceptions;

public abstract class AppException : Exception
{
    protected AppException(string message) : base(message) { }
}

public class NotFoundException : AppException
{
    public NotFoundException(string entity, Guid id)
        : base($"{entity} з Id {id} не знайдено.") { }
}

public class BadRequestException : AppException
{
    public BadRequestException(string message) : base(message) { }
}

public class ConflictException : AppException
{
    public ConflictException(string message) : base(message) { }
}