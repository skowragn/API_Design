namespace Web_Api_Controllers.DTOs.Cart;
public record CreateCartDto
{
    public int CartId { get; set; }
    public string? UserId { get; set; }
    public IEnumerable<CartItemDto> Items { get; set; } = []; 
}
