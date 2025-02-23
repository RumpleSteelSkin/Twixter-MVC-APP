namespace Twixter.Service.Exceptions;

public class UserLockedOutException : Exception
{
    public UserLockedOutException(string message) : base(message) { }
}