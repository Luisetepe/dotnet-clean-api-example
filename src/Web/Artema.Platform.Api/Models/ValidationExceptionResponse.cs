namespace Artema.Platform.Api.Models;

public record ValidationExceptionResponse
{
    public int StatusCode { get; set; }
    public string Type { get; set; } = default!;
    public string Message { get; set; } = default!;
    public Dictionary<string, string[]>? Errors { get; set; } = null;
}