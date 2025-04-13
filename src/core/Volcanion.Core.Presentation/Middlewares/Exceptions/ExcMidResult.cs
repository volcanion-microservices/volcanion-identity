using System.Net;

namespace Volcanion.Core.Presentation.Middlewares.Exceptions
{
    public class ExcMidResult
    {
        public HttpStatusCode ErrorCode { get; set; }

        public string ErrorStatus { get; set; } = null!;

        public string ErrorMessage { get; set; } = null!;

        public string ErrorDetails { get; set; } = null!;

        public string? StackTrace { get; set; } = null!;
    }
}
