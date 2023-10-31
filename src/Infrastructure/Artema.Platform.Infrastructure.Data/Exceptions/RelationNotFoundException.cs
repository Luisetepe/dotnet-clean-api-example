namespace Artema.Platform.Infrastructure.Data.Exceptions;

public class RelationNotFoundException : Exception
{
    public RelationNotFoundException(string entityName, string relationColumn, string value)
        : base($"Could not find relation for entity '{entityName}' property '{relationColumn}' with value '{value}'"){}
}