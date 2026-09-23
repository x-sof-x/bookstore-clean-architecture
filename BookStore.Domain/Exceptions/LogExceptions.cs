namespace BookStore.Domain.Exceptions
{
    public class InfoExceptions : Exception
    {
        public InfoExceptions(string message) : base(message)
        {
        }
    }
    public class DebugException : Exception
    {
        public DebugException(string message) : base(message) { }
    }

    public class WarningException : Exception
    {
        public WarningException(string message) : base(message) { }
    }

    public class CustomErrorException : Exception
    {
        public CustomErrorException(string message) : base(message) { }
    }
}
