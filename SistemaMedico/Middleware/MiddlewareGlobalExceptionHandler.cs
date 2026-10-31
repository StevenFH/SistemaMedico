using System.Net;

namespace SistemaMedico.Middleware
{
    public class MiddlewareGlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MiddlewareGlobalExceptionHandler> _logger;

        public MiddlewareGlobalExceptionHandler(RequestDelegate next, ILogger<MiddlewareGlobalExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            //Logueamos el error internamente
            _logger.LogError(exception, exception.Message);

            ExceptionResponse response = exception switch
            {
                //Captura la regla de negocio de CategoriaService
                InvalidOperationException => new ExceptionResponse(HttpStatusCode.BadRequest, exception.Message),
                KeyNotFoundException => new ExceptionResponse(HttpStatusCode.NotFound, exception.Message),
                UnauthorizedAccessException => new ExceptionResponse(HttpStatusCode.Unauthorized, "No tiene permisos para acceder a este recurso"),
                _ => new ExceptionResponse(HttpStatusCode.InternalServerError, "Ocurrió un error inesperado.")
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)response.StatusCode;
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}