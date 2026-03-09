namespace Domain.Exceptions;

public class SendGridException : Exception
{
    public  SendGridException()
    {}
    public SendGridException(string message) : base(message)
    {}
}