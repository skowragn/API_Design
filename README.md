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


## REST Client and gRpc API (ASP.NET Core 8 gRPC service)

<img width="945" height="534" alt="image" src="https://github.com/user-attachments/assets/7429a190-bce4-418f-8f54-3cfa23f9955f" />


<img width="945" height="469" alt="image" src="https://github.com/user-attachments/assets/fb8fd85e-2085-4bb6-8df5-8ec89507cace" />
