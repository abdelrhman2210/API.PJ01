using API_PJ01_Domain.Exceptions.NotFound;
using API_PJ01_Shared.ErrorModels;

namespace API_PJ01_Web.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
                if (context.Response.StatusCode == 404) // routing middleware
                {
                    context.Response.ContentType = "application/json";
                    var response = new ErrorDetails()
                    {
                        StatusCode = context.Response.StatusCode,
                        ErrorMessage = $"endpoint {context.Request.Path} was not found !!"
                    };

                    await context.Response.WriteAsJsonAsync(response);
                }
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };

                context.Response.ContentType = "application/json";
                var response = new ErrorDetails()
                {
                    StatusCode = context.Response.StatusCode,
                    ErrorMessage = ex.Message
                };

                //return response
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
