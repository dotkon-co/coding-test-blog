using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace CodingBlog.Infrastructure.Extensions;

public static class ValidationErrorHelper
{
    public static ProblemDetails CreateValidationError(this string errorMessage,
        HttpStatusCode? statusCode = HttpStatusCode.BadRequest)
    {
        return new ProblemDetails
        {
            Type = "Validation",
            Title = "One or more validation errors occurred.",
            Status = (int)statusCode!,
            Detail = errorMessage,
        };
    }
}