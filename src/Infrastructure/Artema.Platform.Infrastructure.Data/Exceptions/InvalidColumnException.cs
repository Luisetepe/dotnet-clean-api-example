namespace Artema.Platform.Infrastructure.Data.Exceptions;

public class InvalidColumnException : Exception
{
    public InvalidColumnException(string entityName, string columnName)
        : base($"Entity '{entityName}' does not contain column with name '{columnName}'"){}
}