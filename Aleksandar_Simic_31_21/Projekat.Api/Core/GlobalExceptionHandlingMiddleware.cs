using FluentValidation;
using ProjekatApplication;
using ProjekatImplementation.Exceptions;

namespace Projekat.Api.Core
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private IApplicationActor _actor;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, IApplicationActor actor)
        {
            _next = next;
            _actor = actor;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                httpContext.Response.ContentType = "application/json";
                if (exception is UnauthorizedException)
                {
                    httpContext.Response.StatusCode = 401;
                    await httpContext.Response.WriteAsJsonAsync("You are not allowed to execute this command");
                    return;
                }

                if (exception is ValidationException ex)
                {
                    httpContext.Response.StatusCode = 422;
                    var body = ex.Errors.Select(x => new { Property = x.PropertyName, Error = x.ErrorMessage });

                    await httpContext.Response.WriteAsJsonAsync(body);
                    return;
                }

                if(exception is KeyNotFoundException)
                {
                    httpContext.Response.StatusCode = 404;
                    await httpContext.Response.WriteAsJsonAsync("Resource not found");
                    return;
                }

                if(exception is InvalidOperationException)
                {
                    httpContext.Response.StatusCode = 409;
                    await httpContext.Response.WriteAsJsonAsync("This resource is not allowed to be removed");
                    return;
                }
                httpContext.Response.StatusCode = 500;
                await httpContext.Response.WriteAsJsonAsync(new { Message = $"An unexpected error has occured." });
            }
        }
    }
}
