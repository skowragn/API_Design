# API_Design
# The Align-Define-Design-Refine Process
<img width="1125" height="1036" alt="image" src="https://github.com/user-attachments/assets/276e9e62-7a4c-42ad-b922-13310bc96a8e" />


## Step 1. Identify Digital Capabilities
## Step 2. Capture Activities and Steps
## Step 3. Identify the Activities for Each Job Story
- Event Storming
- https://www.eventstorming.com

  <img width="1054" height="498" alt="image" src="https://github.com/user-attachments/assets/3235cde7-ca4d-4726-9f46-14c59b0968e7" />

## Step 4. Identifying API Boundaries

## Step 5. The API Modeling Process

### 1. REST API Design Process

| Command | Description |
| --- | --- |
|List, Search, View All|Get /books|
|Show, Retrieve, View| Get /Books/{bookId}|
|Create, Add|Post /books|
|Replace| Put /carts/{cartId}|
|| Put /carts/{cartId}/items|
|Update| Patch /carts/{cartId}|
|Delete All, Remove All, Clear, Reset|Delete /carts/{cartId}/items|
|Delete |Delete /carts/{cartId}/items/{cartItemId}|
|Search |Post /carts/search|
|Other verbs |Post /books/{bookId}/deactivate|


#### Step 1: Design Resource URL Paths

| Resource | URL |
| --- | --- |
| Book | /books | 
| Cart |/carts|
| Cart Items |/carts/{cartId}/Items|
| Author  |/author|

#### Step 2: Map API Operations to HTTP Methods
#### Step 3: Assign Response Codes
| Resource Path | Http Method | Request | Response | Response Code|
| --- | --- | --- | --- |--- |
| /books | GET | | Books | 200 |
| /books/{bookId} | GET | bookId | Book | 200 OK, 404 Not Found|
| /books | POST | Book | Book | 201 Created, 400 Bad Request |
| /books/search | POST | Query | Books | 200 OK |
| /books/{bookId} | PUT | bookId |  | 201 Created, 404 Not Found, 204 No Content |
| /books/{bookId} | DELETE | bookId |  | 404 Not Found, 204 No Content |
| /authors | GET |  | Authors | 200 OK|
| /carts/{cartId} | GET | cartId | Cart | 200 OK, 404 Not Found|
| /carts/{cartId}/items | POST | Cart, cartId | Book | 201 Created, 400 Bad Request |
| /carts/{cartId} | DELETE | cartId |  | 404 Not Found, 204 No Content |
| /carts/{cartId}/items/{cartItemId} | DELETE | cartId, cartItemId |  | 404 Not Found, 204 No Content |

#### Step 4: Documenting the REST API Design
#### Step 5: Share and Gather Feedback

# Results
## REST API with minimal API (ASP.NET Core 8 Web Api)

<img width="945" height="387" alt="image" src="https://github.com/user-attachments/assets/ded5ca8b-a370-48b2-b9fe-0952d0bba5af" />


<img width="945" height="476" alt="image" src="https://github.com/user-attachments/assets/7b7b4b07-ebe3-4420-8ff8-8f941866db4c" />

## REST API with MVC controllers (ASP.NET Core 8 Web Api)

<img width="945" height="499" alt="image" src="https://github.com/user-attachments/assets/efd8b523-7646-4b74-9a45-13455f90417c" />


<img width="945" height="531" alt="image" src="https://github.com/user-attachments/assets/97653fc4-20e4-4d13-b443-1def806b4de1" />
<img width="945" height="74" alt="image" src="https://github.com/user-attachments/assets/0f100b33-b4ad-4b48-a822-522253e778d5" />

### 2. Grpc API Design Process

| Operation Name | Request | Response |
| --- | --- |--- |
|listBooks()|**GetBooks (AllBookRequest)** - List all books| **AllBookReply** – **Books[]** or **google.rpc.Status + ProblemDetails**|
|viewbook()|**GetBookById (BookRequest)** - bookId| **BookReply** – **Book** or **google.rpc.Status + ProblemDetails**|
|addBook()|**CreateBooks (CreateBookRequest)** - new Book| **CreateBookReply** – **Book with new Id** or **google.rpc.Status + ProblemDetails**|
|deleteBook()|**DeleteBookById (BookRequest)** - bookId| **BookReply** – **deleted Book info** or **google.rpc.Status + ProblemDetails**|
||||
|listAuthors()|**GetAuthors (AllAuthorRequest)** - List all authors| **AllAuthorReply** – **Authors[]** or **google.rpc.Status + ProblemDetails**|
||||
|viewCart()|**GetCartById (CartRequest)** - cartId| **CartReply** – **Cart** or **google.rpc.Status + ProblemDetails**|
|addCart()|**CreateCart (CreateCartRequest)** - new Cart| **CreateCartReply** – **Cart with new Id** or **google.rpc.Status + ProblemDetails**|
|deleteCart()|**DeleteCartById (CartRequest)** - cartId| **CartReply** – **deleted Cart info** or **google.rpc.Status + ProblemDetails**|
|deleteBookInCartUser()|**DeleteCartItemByIndex (DeleteCartItemRequest)** - cartId, cartItemId| **CartReply** – **updated Cart** or **google.rpc.Status + ProblemDetails**|

book.proto3
```
service Book {

 rpc GetBooks (AllBookRequest) returns (AllBookReply); 
 rpc GetBookById (BookRequest) returns (BookReply);
 rpc CreateBook (CreateBookRequest) returns (CreateBookReply);
 rpc DeleteBookById(BookRequest) returns (BookReply);
}

message BookRequest {
  int32 bookId = 1;
}

message BookReply {
  int32 bookId = 1;
  string isbn = 2;
  string title = 3;
  string description = 4;
  repeated AuthorTypeReply authors = 5;
}

message AuthorTypeReply {
  int32 authorId = 1;
  string fullName = 2;
}

message AllBookRequest {
}

message AllBookReply {
  repeated BookReply books = 1;
}

message CreateBookReply {
  int32 bookId = 1;
  string isbn = 2;
  string title = 3;
  string description = 4;
  repeated AuthorTypeReply authors = 5;
}

message CreateBookRequest {
  string isbn = 1;
  string title = 2;
  string description = 3;
  repeated AuthorTypeReply authors = 4;
}

```
author.proto3
```
service Author {

  rpc GetAuthors (AllAuthorRequest) returns (AllAuthorReply);
}

message AuthorRequest {
  int32 authorId = 1;
}

message AuthorReply {
  int32 authorId = 1;
  string fullName = 2;
}

message AllAuthorRequest {
}

message AllAuthorReply {
  repeated AuthorReply authors = 1;
}
```
cart.proto3
```
service Cart {

 rpc GetCartById (CartRequest) returns (CartReply);
 rpc CreateCart (CreateCartRequest) returns (CreateCartReply);
 rpc DeleteCartById(CartRequest) returns (CartReply);
 rpc DeleteCartItemByIndex(DeleteCartItemRequest) returns (CartReply);
}

message CartRequest {
  int32 cartId = 1;
}

message CartReply {
  int32 cartId = 1;
  string userId = 2;
  repeated CartItemReply Items = 3; 
}

message CartItemReply
{
   int32 cartItemId = 1;    
   CartBookTypeReply Book = 2;
   int32 Quantity = 3;
   double Price = 4;
}

message CartBookTypeReply {
  int32 bookId = 1;
  string isbn = 2;
  string title = 3;
  string description = 4;
  repeated CartAuthorTypeReply authors = 5;
}

message CartAuthorTypeReply {
  int32 authorId = 1;
  string fullName = 2;
}

message CreateCartRequest {
  string userId = 2;
  repeated CartItemReply Items = 3; 
}

message CreateCartReply {
  int32 cartId = 1;
  string userId = 2;
  repeated CartItemReply Items = 3; 
}

message DeleteCartItemRequest {
  int32 cartId = 1;
  int32 cartItemId = 2;
}
```
## REST Client and gRpc API (ASP.NET Core 8 gRPC service)

<img width="945" height="534" alt="image" src="https://github.com/user-attachments/assets/7429a190-bce4-418f-8f54-3cfa23f9955f" />


<img width="945" height="469" alt="image" src="https://github.com/user-attachments/assets/fb8fd85e-2085-4bb6-8df5-8ec89507cace" />
