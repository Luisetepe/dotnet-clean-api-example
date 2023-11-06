namespace Artema.Platform.Api.Endpoints.GetProductSearchConfiguration;

public record GetProductSearchConfigurationResponse
{
    public string Endpoint { get; set; }  = default!;
    public Dictionary<string, string[]> FilterFields { get; set; } = default!;
    public IEnumerable<string> OrderByFields { get; set; } = default!;
}