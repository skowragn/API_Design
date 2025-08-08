namespace Grpc.Sdk.DTOs;
public record CartRestRequest
{
    public int CartId { get; set; }
}

public record CreateCartRestRequest
{
    public int UserId { get; set; }
    public IEnumerable<CartRestItem> Items { get; set; } = [];
}
