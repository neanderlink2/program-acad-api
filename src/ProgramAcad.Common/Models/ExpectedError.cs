using System.Net;

namespace ProgramAcad.Common.Models
{
    public class ExpectedError
    {
        public ExpectedError(HttpStatusCode statusCode, string source, string detail)
        {
            StatusCode = statusCode;
            Source = source;
            Detail = detail;
        }

        public ExpectedError(HttpStatusCode statusCode, string error)
        {
            StatusCode = statusCode;            
            Detail = error;
        }

        public HttpStatusCode StatusCode { get; protected set; }
        public string Source { get; protected set; }
        public string Detail { get; protected set; }
    }
}
