using System.Net;
using System.Text.Json.Serialization;

namespace FG.Result;



public class Response<T>
{
    [JsonPropertyName("data")] public T? Data { get; private set; }

    [JsonPropertyName("errors")] public List<string>? Errors { get; private set; }

    [JsonPropertyName("success")] public bool Success { get; private set; }

    [JsonPropertyName("statusCode")] public int StatusCode { get; private set; }

    private Response()
    {
    }

    public static Response<T> Succeed(T data, int statusCode = (int)HttpStatusCode.OK)
    {
        return new Response<T> { Success = true, Data = data, StatusCode = statusCode };
    }

    public static Response<string> SucceedCreate(T data, int statusCode = (int)HttpStatusCode.Created) // 201 Created
    {
        string message = $"{typeof(T).Name} is created";
        return new Response<string> { Success = true, Data = message, StatusCode = statusCode };
    }
    public static Response<string> SucceedDelete(T data, int statusCode = (int)HttpStatusCode.OK)
    {
        string message = $"{typeof(T).Name} is deleted";
        return new Response<string> { Success = true, Data = message, StatusCode = statusCode };
    }

    public static Response<string> SucceedRemove(T data, int statusCode = (int)HttpStatusCode.OK)
    {
        string message = $"{typeof(T).Name} is removed";
        return new Response<string> { Success = true, Data = message, StatusCode = statusCode };
    }


    public static Response<T> Fail(List<string> errors, int statusCode = (int)HttpStatusCode.InternalServerError)
    {
        return new Response<T> { Success = false, Errors = errors, StatusCode = statusCode };
    }

    public static Response<T> Fail(string error, int statusCode = (int)HttpStatusCode.InternalServerError)
    {
        return new Response<T> { Success = false, Errors = new List<string> { error }, StatusCode = statusCode };
    }


    // Fluent Interface
    public Response<T> WithData(T data)
    {
        Data = data;
        return this;
    }

    public Response<T> WithErrors(List<string> errors)
    {
        Errors = errors;
        return this;
    }

    public Response<T> WithError(string error)
    {
        Errors ??= new List<string>();
        Errors.Add(error);
        return this;
    }

    public Response<T> WithStatusCode(int statusCode)
    {
        StatusCode = statusCode;
        return this;
    }
}


public class Response
{
    [JsonPropertyName("data")]
    public object? Data { get; private set; }

    [JsonPropertyName("errors")]
    public List<string>? Errors { get; private set; }

    [JsonPropertyName("success")]
    public bool Success { get; private set; }

    [JsonPropertyName("statusCode")]
    public int StatusCode { get; private set; }


    private Response() { }

    public static Response Succeed(object? data = null, int statusCode = (int)HttpStatusCode.OK)
    {
        return new Response { Success = true, Data = data, StatusCode = statusCode };
    }

    public static Response Fail(List<string> errors, int statusCode = (int)HttpStatusCode.InternalServerError)
    {
        return new Response { Success = false, Errors = errors, StatusCode = statusCode };
    }

    public static Response Fail(string error, int statusCode = (int)HttpStatusCode.InternalServerError)
    {
        return new Response { Success = false, Errors = new List<string> { error }, StatusCode = statusCode };
    }

    public static Response SucceedCreate<T>(int statusCode = (int)HttpStatusCode.Created) // 201 Created
    {
        string message = $"{typeof(T).Name} is created";
        return new Response { Success = true, Data = message, StatusCode = statusCode };
    }

    public static Response SucceedUpdate<T>(int statusCode = (int)HttpStatusCode.OK)
    {
        string message = $"{typeof(T).Name} is updated";
        return new Response { Success = true, Data = message, StatusCode = statusCode };
    }

    public static Response SucceedDelete<T>(int statusCode = (int)HttpStatusCode.OK)
    {
        string message = $"{typeof(T).Name} is deleted";
        return new Response { Success = true, Data = message, StatusCode = statusCode };
    }

    public static Response SucceedRemove<T>(int statusCode = (int)HttpStatusCode.OK)
    {
        string message = $"{typeof(T).Name} is removed";
        return new Response { Success = true, Data = message, StatusCode = statusCode };
    }

    // Fluent Interface
    public Response WithData(object? data) { Data = data; return this; }
    public Response WithErrors(List<string> errors) { Errors = errors; return this; }
    public Response WithError(string error) { Errors ??= new List<string>(); Errors.Add(error); return this; }
    public Response WithStatusCode(int statusCode) { StatusCode = statusCode; return this; }
}
