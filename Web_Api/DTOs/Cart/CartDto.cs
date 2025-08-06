namespace Web_Api.DTOs.Cart;
public class CartDto
{
    public int CartId { get; set; }
    public string? UserId { get; set; }
    public IEnumerable<CartItemDto> Items { get; set; } = []; 
}
