namespace com.mobiquity.packer.Exceptions;

public class APIException : Exception
{
    public APIException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public APIException(string message)
        : base(message)
    {
    }
}