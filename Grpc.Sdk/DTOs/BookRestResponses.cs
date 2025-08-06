namespace Grpc.Sdk.DTOs;
public record BookRestResponses
{
    public int BookId { get; set; }
    public string? Isbn { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public IEnumerable<AuthorRestResponses> Authors { get; set; } = [];
}
