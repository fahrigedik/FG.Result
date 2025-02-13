# FG.Result - Generic API Response Structure for .NET

[![NuGet](https://img.shields.io/nuget/v/FG.Result.svg)](https://www.nuget.org/packages/FG.Result/)

`FG.Result` is a lightweight and easy-to-use library for .NET projects that provides a consistent and structured way to handle API responses.  It includes properties for success status, data, error messages, and HTTP status codes.  This eliminates the need to create custom response classes for each API endpoint and promotes consistency across your projects.

## Features

*   Static methods for creating success and failure responses (`Succeed`, `Fail`).
*   Dedicated methods for common CRUD operations (`SucceedCreate`, `SucceedUpdate`, `SucceedDelete`, `SucceedRemove`).
*   Error message list for detailed error reporting.
*   HTTP status code for standard response handling.
*   Generic (`Response<T>`) and non-generic (`Response`) classes to cover both cases where data is returned and where it isn't.
*   Fluent API support (optional) for easy response modification.
*   Lightweight and dependency-free.
*   Fully documented with XML documentation comments.
*   Fully tested with Unit Tests.

## Installation

Using the NuGet Package Manager Console:

```powershell
Install-Package FG.Result
Using the .NET CLI

Using the .NET CLI:

Bash

dotnet add package FG.Result
Usage Examples
C#

using TS.Result; // Or your project's namespace
using System.Net;

// Successful response (with data)
var user = new User { Id = 1, Name = "John Doe" };
var successResponse = Response<User>.Succeed(user);

// Successful response (no data)
var successResponseNoData = Response.Succeed();

// Failed response (single error message)
var errorResponse = Response.Fail("User not found", (int)HttpStatusCode.NotFound);

// Failed response (multiple error messages)
var multipleErrorsResponse = Response.Fail(new List<string> { "Error 1", "Error 2" });

// CRUD operations
var createdResponse = Response.SucceedCreate<User>(); // "User is created"
var updatedResponse = Response.SucceedUpdate<Product>(); // "Product is updated"

// Fluent API
var fluentResponse = Response.Fail("Invalid request")
    .WithStatusCode(400)
    .WithError("Field 'email' cannot be empty");

// Example with custom error codes (if you use the ErrorCode enum):
// var errorCodeResponse = Response.Fail("Invalid input", errorCode: ErrorCode.BadRequest);

Response<T> Class
C#

public class Response<T>
{
    public T? Data { get; private set; }
    public List<string>? Errors { get; private set; }
    public bool Success { get; private set; }
    public int StatusCode { get; private set; }
   //...
}
Response Class (Non-Generic)
C#

public class Response
{
    public object? Data { get; private set; }
    public List<string>? Errors { get; private set; }
    public bool Success { get; private set; }
    public int StatusCode { get; private set; }
   //...
}
Contributing
Contributions are welcome!  Please see the CONTRIBUTING.md file (if you have one) for details on how to contribute.  You can also open issues on the GitHub repository.  (Replace with your actual GitHub repository URL).

License
This project is licensed under the MIT License. See the LICENSE file for details. (Or whichever license you choose.)



