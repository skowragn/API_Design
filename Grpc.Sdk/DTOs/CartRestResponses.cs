namespace Grpc.Sdk.DTOs;
public record CartRestResponses
{
    public int CartId { get; set; }
    public string? UserId { get; set; }
    public List<CartRestItem> Items { get; set; } = [];
}

public record CartRestItem
{
    public int CartItemId { get; set; }
    public BookRestResponses Book { get; set; } = new();
    public int Quantity { get; set; }
    public double Price { get; set; }
}

public record CreateCartRestResponses
{
    public int CartId { get; set; }
    public string? UserId { get; set; }
    public IEnumerable<CartRestItem> Items { get; set; } = [];
}