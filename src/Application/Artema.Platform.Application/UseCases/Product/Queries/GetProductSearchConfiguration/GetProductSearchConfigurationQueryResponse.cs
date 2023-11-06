namespace Artema.Platform.Application.UseCases.Queries.GetProductSearchConfiguration;

public record GetProductSearchConfigurationQueryResponse
{
    public Dictionary<string, string[]> FilterFields { get; set; } = default!;
    public IEnumerable<string> OrderByFields { get; set; } = default!;
}