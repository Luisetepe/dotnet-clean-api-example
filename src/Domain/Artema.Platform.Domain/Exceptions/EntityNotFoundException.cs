namespace Artema.Platform.Domain.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string modelName, string columnName, string value)
            : base($"Could not find entity '{modelName}' by property '{columnName}' with value '{value}'")
        {
        }
}
