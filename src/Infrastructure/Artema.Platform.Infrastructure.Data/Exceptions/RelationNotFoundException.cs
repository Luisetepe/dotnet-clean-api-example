namespace Artema.Platform.Infrastructure.Data.Exceptions;

public class RelationNotFoundException : Exception
{
    public RelationNotFoundException(string modelName, string relationColumn, string value)
        : base($"Could not find relation for table '{modelName}' property '{relationColumn}' with value '{value}'"){}
}
