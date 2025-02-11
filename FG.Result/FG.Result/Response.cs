using System.Net;
using System.Text.Json.Serialization;

namespace FG.Result;


/// <summary>
/// Provides a generic response structure for APIs. Includes success/failure status, data, error messages, and status code.
/// This class is used for generic operations that DO return a specific data type.
/// </summary>
/// <typeparam name="T">The type of the data returned by the operation.</typeparam>
public class Response<T>
{
    /// <summary>
    /// Represents the data returned by the operation.
    /// </summary>
    [JsonPropertyName("data")] public T? Data { get; private set; }

    /// <summary>
    /// If the operation fails, this list contains error messages. If successful, it may be null or empty.
    /// </summary>
    [JsonPropertyName("errors")] public List<string>? Errors { get; private set; }

    /// <summary>
    /// Indicates whether the operation was successful.
    /// </summary>
    [JsonPropertyName("success")] public bool Success { get; private set; }

    /// <summary>
    /// Indicates the HTTP status code related to the operation (e.g., 200, 400, 404).
    /// </summary>
    [JsonPropertyName("statusCode")] public int StatusCode { get; private set; }

    private Response()
    {
    }

    /// <summary>
    /// Creates a successful response.
    /// </summary>
    /// <typeparam name="T">The type of the data returned by the operation.</typeparam>
    /// <param name="data">The data returned by the operation.</param>
    /// <param name="statusCode">The HTTP status code (default: 200 OK).</param>
    /// <returns>A successful Response&lt;T&gt; object.</returns>
    public static Response<T> Succeed(T data, int statusCode = (int)HttpStatusCode.OK)
    {
        return new Response<T> { Success = true, Data = data, StatusCode = statusCode };
    }

    /// <summary>
    /// Creates a successful response for an update operation.
    /// The Data property will contain a message like "TypeName is updated".
    /// </summary>
    /// <typeparam name="T">The type of the entity that was updated.</typeparam>
    /// <param name="data">The updated entity (kept for consistency, but the actual returned data is a string message).</param>
    /// <param name="statusCode">The HTTP status code (default: 200 OK).</param>
    /// <returns>A successful Response&lt;string&gt; object with an update message.</returns>
    public static Response<string> SucceedUpdate(T data, int statusCode = (int)HttpStatusCode.OK)
    {
        string message = $"{typeof(T).Name} is updated";
        return new Response<string> { Success = true, Data = message, StatusCode = statusCode };
    }

    /// <summary>
    /// Creates a successful response indicating that a new entity of type T has been created.
    /// The Data property of the response will contain a message like "TypeName is created".
    /// </summary>
    /// <typeparam name="T">The type of the entity that was created.</typeparam>
    /// <param name="data">The created entity (although not directly used in the response, it's kept for consistency and potential future use).</param>
    /// <param name="statusCode">The HTTP status code (default: 201 Created).</param>
    /// <returns>A successful Response&lt;string&gt; object with a creation message.</returns>
    public static Response<string> SucceedCreate(T data, int statusCode = (int)HttpStatusCode.Created) // 201 Created
    {
        string message = $"{typeof(T).Name} is created";
        return new Response<string> { Success = true, Data = message, StatusCode = statusCode };
    }

    /// <summary>
    /// Creates a successful response for a delete operation, returning a "TypeName is deleted" message.
    /// </summary>
    /// <typeparam name="T"> The type of the entity that was deleted.</typeparam>
    /// <param name="data">The data of type T that represent deleted entity.</param>
    /// <param name="statusCode">The HTTP status code, with a default of 200 (OK).</param>
    /// <returns>A successful <see cref="Response{String}"/>, indicating the deletion.</returns>
    public static Response<string> SucceedDelete(T data, int statusCode = (int)HttpStatusCode.OK)
    {
        string message = $"{typeof(T).Name} is deleted";
        return new Response<string> { Success = true, Data = message, StatusCode = statusCode };
    }

    /// <summary>
    /// Creates a successful response for a remove operation.  This is often synonymous with deletion.
    /// The returned Data will contain a message like "TypeName is removed".
    /// </summary>
    /// <typeparam name="T">The type of the entity that was removed.</typeparam>
    /// <param name="data">The removed entity (kept for consistency).</param>
    /// <param name="statusCode">The HTTP status code (default: 200 OK).</param>
    /// <returns>A successful Response&lt;string&gt; object with a removal message.</returns>
    public static Response<string> SucceedRemove(T data, int statusCode = (int)HttpStatusCode.OK)
    {
        string message = $"{typeof(T).Name} is removed";
        return new Response<string> { Success = true, Data = message, StatusCode = statusCode };
    }


    /// <summary>
    /// Creates a failed response with multiple error messages.
    /// </summary>
    /// <typeparam name="T">The type of the data expected to be returned by the operation (usually not used when failed).</typeparam>
    /// <param name="errors">A list of error messages.</param>
    /// <param name="statusCode">The HTTP status code (default: 500 Internal Server Error).</param>
    /// <returns>A failed Response&lt;T&gt; object.</returns>
    public static Response<T> Fail(List<string> errors, int statusCode = (int)HttpStatusCode.InternalServerError)
    {
        return new Response<T> { Success = false, Errors = errors, StatusCode = statusCode };
    }

    /// <summary>
    /// Creates a failed response with a single error message.
    /// </summary>
    /// <typeparam name="T">The type of the data expected to be returned by the operation.</typeparam>
    /// <param name="error">The error message.</param>
    /// <param name="statusCode">The HTTP status code (default: 500 Internal Server Error).</param>
    /// <returns>A failed Response&lt;T&gt; object.</returns>
    public static Response<T> Fail(string error, int statusCode = (int)HttpStatusCode.InternalServerError)
    {
        return new Response<T> { Success = false, Errors = new List<string> { error }, StatusCode = statusCode };
    }


    // Fluent Interface

    /// <summary>
    /// Adds data to the response
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public Response<T> WithData(T data)
    {
        Data = data;
        return this;
    }

    /// <summary>
    /// Adds error list to the response
    /// </summary>
    /// <param name="errors"></param>
    /// <returns></returns>
    public Response<T> WithErrors(List<string> errors)
    {
        Errors = errors;
        return this;
    }

    /// <summary>
    /// Adds error message to the response
    /// </summary>
    /// <param name="error"></param>
    /// <returns></returns>
    public Response<T> WithError(string error)
    {
        Errors ??= new List<string>();
        Errors.Add(error);
        return this;
    }

    /// <summary>
    /// Adds status code to the response
    /// </summary>
    /// <param name="statusCode"></param>
    /// <returns></returns>
    public Response<T> WithStatusCode(int statusCode)
    {
        StatusCode = statusCode;
        return this;
    }
}


/// <summary>
/// Provides a generic response structure for APIs. Includes success/failure status, error messages, and status code.
/// This class is used for non-generic operations that DO NOT return a specific data type.
/// </summary>
public class Response
{
    /// <summary>
    /// Represents the data returned by the operation. Since this class is not generic,
    /// the type of Data is object?. Therefore, you may need to cast it when using it.
    /// </summary>

    [JsonPropertyName("data")]
    public object? Data { get; private set; }

    /// <summary>
    /// If the operation fails, this list contains error messages. If successful, it may be null or empty.
    /// </summary>

    [JsonPropertyName("errors")]
    public List<string>? Errors { get; private set; }

    /// <summary>
    /// Indicates whether the operation was successful.
    /// </summary>

    [JsonPropertyName("success")]
    public bool Success { get; private set; }

    /// <summary>
    /// Indicates the HTTP status code related to the operation (e.g., 200, 400, 404).
    /// </summary>

    [JsonPropertyName("statusCode")]
    public int StatusCode { get; private set; }


    private Response() { }

    /// <summary>
    /// Creates a successful response.
    /// </summary>
    /// <param name="data">The data returned by the operation (optional).  You can often return a message or a simple value with this non-generic Response class.</param>
    /// <param name="statusCode">The HTTP status code (default: 200 OK).</param>
    /// <returns>A successful Response object.</returns>

    public static Response Succeed(object? data = null, int statusCode = (int)HttpStatusCode.OK)
    {
        return new Response { Success = true, Data = data, StatusCode = statusCode };
    }

    /// <summary>
    /// Creates a failed response with multiple error messages.
    /// </summary>
    /// <param name="errors">A list of error messages.</param>
    /// <param name="statusCode">The HTTP status code (default: 500 Internal Server Error).</param>
    /// <returns>A failed Response object.</returns>
    public static Response Fail(List<string> errors, int statusCode = (int)HttpStatusCode.InternalServerError)
    {
        return new Response { Success = false, Errors = errors, StatusCode = statusCode };
    }

    /// <summary>
    /// Creates a failed response with a single error message.
    /// </summary>
    /// <param name="error">The error message.</param>
    /// <param name="statusCode">The HTTP status code (default: 500 Internal Server Error).</param>
    /// <returns>A failed Response object.</returns>
    public static Response Fail(string error, int statusCode = (int)HttpStatusCode.InternalServerError)
    {
        return new Response { Success = false, Errors = new List<string> { error }, StatusCode = statusCode };
    }


    /// <summary>
    /// Creates a successful response used when a new entity is created (Create).
    /// </summary>
    /// <typeparam name="T">The type of the created entity.</typeparam>
    /// <param name="statusCode">The HTTP status code. Default: 201 Created</param>
    /// <returns>A successful Response object.</returns>
    public static Response SucceedCreate<T>(int statusCode = (int)HttpStatusCode.Created) // 201 Created
    {
        string message = $"{typeof(T).Name} is created";
        return new Response { Success = true, Data = message, StatusCode = statusCode };
    }

    /// <summary>
    /// Creates a successful response used when an entity is updated (Update).
    /// </summary>
    /// <typeparam name="T">The type of the updated entity.</typeparam>
    /// <param name="statusCode">The HTTP status code.</param>
    /// <returns>A successful Response object.</returns>
    public static Response SucceedUpdate<T>(int statusCode = (int)HttpStatusCode.OK)
    {
        string message = $"{typeof(T).Name} is updated";
        return new Response { Success = true, Data = message, StatusCode = statusCode };
    }


    /// <summary>
    /// Creates a successful response used when an entity is deleted (Delete).
    /// </summary>
    /// <typeparam name="T">The type of the deleted entity.</typeparam>
    /// <param name="statusCode">The HTTP status code.</param>
    /// <returns>A successful Response object.</returns>
    public static Response SucceedDelete<T>(int statusCode = (int)HttpStatusCode.OK)
    {
        string message = $"{typeof(T).Name} is deleted";
        return new Response { Success = true, Data = message, StatusCode = statusCode };
    }

    /// <summary>
    /// Creates a successful response used when an entity is removed (Remove). Usually used with the same meaning as Delete.
    /// </summary>
    /// <typeparam name="T">The type of the removed entity.</typeparam>
    /// <param name="statusCode">The HTTP status code.</param>
    /// <returns>A successful Response object.</returns>
    public static Response SucceedRemove<T>(int statusCode = (int)HttpStatusCode.OK)
    {
        string message = $"{typeof(T).Name} is removed";
        return new Response { Success = true, Data = message, StatusCode = statusCode };
    }

    // Fluent Interface 
    /// <summary>
    /// Adds data to the response.
    /// </summary>
    /// <param name="data">The data to be added.</param>
    /// <returns>The modified Response object.</returns>
    public Response WithData(object? data) { Data = data; return this; }

    /// <summary>
    /// Adds error messages to the response.
    /// </summary>
    /// <param name="errors">The list of error messages.</param>
    /// <returns>The modified Response object.</returns>
    public Response WithErrors(List<string> errors) { Errors = errors; return this; }

    /// <summary>
    /// Adds a single error message to the response.
    /// </summary>
    /// <param name="error">The error message.</param>
    /// <returns>The modified Response object.</returns>
    public Response WithError(string error) { Errors ??= new List<string>(); Errors.Add(error); return this; }

    /// <summary>
    /// Changes the status code of the response.
    /// </summary>
    /// <param name="statusCode">The HTTP status code.</param>
    /// <returns>The modified Response object.</returns>
    public Response WithStatusCode(int statusCode) { StatusCode = statusCode; return this; }
}
