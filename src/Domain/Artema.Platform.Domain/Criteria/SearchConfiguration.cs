namespace Artema.Platform.Domain.Criteria;

public class SearchConfiguration
{
    public Dictionary<string, string[]> FilterFields { get; set; } = default!;
    public IEnumerable<string> OrderByFields { get; set; } = default!;
}