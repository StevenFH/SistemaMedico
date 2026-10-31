using System.Net;

namespace SistemaMedico.Middleware
{
    public record ExceptionResponse(HttpStatusCode StatusCode, string Description);
}