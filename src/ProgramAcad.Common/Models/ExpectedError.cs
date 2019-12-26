using System.Net;

namespace ProgramAcad.Common.Models
{
    public class ExpectedError
    {
        public ExpectedError(string source, string detail)
        {
            Source = source;
            Detail = detail;
        }

        public ExpectedError(string error)
        {
            Detail = error;
        }

        public string Source { get; protected set; }
        public string Detail { get; protected set; }
    }
}
