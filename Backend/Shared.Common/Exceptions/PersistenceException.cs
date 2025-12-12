namespace Shared.Common.Exceptions;
public class PersistenceException : Exception
{
    public PersistenceException(string message, Exception? innerException = null)
        : base(message, innerException) { }
}
