namespace CodingBlog.Presentation.Middlewares;

using System.Net;
using System.Text.Json;
using Serilog;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
   

    public ExceptionHandlerMiddleware(RequestDelegate next) =>
        _next = next;

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception exception)
        {
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            httpContext.Response.ContentType = "application/json";

            await SendLogError(exception, httpContext);
        }
    }
    
    private Task SendLogError(Exception exception, HttpContext httpContext)
    {
        Log.Error(exception, "Erro não tratado na aplicação");

        var response = httpContext.Response;
        response.ContentType = "application/json";
        response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var result = JsonSerializer.Serialize(new 
        {
            error = "Ocorreu um erro interno no servidor. Por favor, tente novamente mais tarde.",
            detail = exception.Message // Remova esta linha para não expor detalhes do erro
        });

        return response.WriteAsync(result);
    }
}