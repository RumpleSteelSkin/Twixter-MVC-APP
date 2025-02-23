namespace Twixter.Service.Exceptions;

public class EmailNotConfirmedException : Exception
{
    public EmailNotConfirmedException(string message) : base(message) { }
}