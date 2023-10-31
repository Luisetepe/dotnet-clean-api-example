namespace Artema.Platform.Api.Models;

public record ExceptionHttpResponse
{
    public int StatusCode { get; set; }
    public string Type { get; set; } = default!;
    public string Message { get; set; } = default!;
}