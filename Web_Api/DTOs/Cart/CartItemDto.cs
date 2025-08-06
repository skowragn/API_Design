using Web_Api.DTOs.Book;

namespace Web_Api.DTOs.Cart;

public class CartItemDto
{
    public int CartItemId { get; set; }
    public required BookDto Book { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; } 
}
