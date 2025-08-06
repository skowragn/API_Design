using Web_Api_Controllers.DTOs.Book;

namespace Web_Api_Controllers.DTOs.Cart;

public class CartItemDto
{
    public int CartItemId { get; set; }
    public required BookDto Book { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; } 
}
