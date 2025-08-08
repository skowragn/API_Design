using Grpc.Sdk.DTOs;

namespace Clients.Mappers;

public static class CartReplyToCartRestResponses
{
    public static CartRestResponses ToCartRestResponses(this Grpc_Api.CartReply cartReply)
    {
        var cartRestResponses = new CartRestResponses()
        {
            CartId = cartReply.CartId,
            UserId = cartReply.UserId,
        };

        var cartRestItems = cartReply.Items.Select(item => item.ToCartRestItem());
        cartRestResponses.Items.AddRange(cartRestItems);

        return cartRestResponses;

    }

    public static CartRestResponses ToCartRestResponses(this Grpc_Api.CreateCartReply createCartReply)
    {
       var cartRestResponses = new CartRestResponses()
           {
              CartId = createCartReply.CartId,
              UserId = createCartReply.UserId,
           };

        var cartRestItems = createCartReply.Items.Select(item => item.ToCartRestItem());
        cartRestResponses.Items.AddRange(cartRestItems);

        return cartRestResponses;
    }

    public static CartRestItem ToCartRestItem(this Grpc_Api.CartItemReply cartItemReply)
    {
        return new CartRestItem()
        {
           CartItemId = cartItemReply.CartItemId,
           Book = new BookRestResponses() 
           {
               BookId = cartItemReply.Book.BookId,
                Isbn = cartItemReply.Book.Isbn,
                Title = cartItemReply.Book.Title,
                Description = cartItemReply.Book.Description,
                Authors = cartItemReply.Book.Authors.Select(author => new AuthorRestResponses()
                {
                    AuthorId = author.AuthorId,
                    FullName = author.FullName
                }).ToList()
           },
           Quantity = cartItemReply.Quantity,
           Price = cartItemReply.Price
        };
    }
}
