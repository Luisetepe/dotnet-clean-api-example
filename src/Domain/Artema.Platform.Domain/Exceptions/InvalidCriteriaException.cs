namespace Artema.Platform.Domain.Exceptions;

public class InvalidCriteriaException : DomainException
{
    public InvalidCriteriaException(string message) : base(message)
    {
    }
}