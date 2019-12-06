using System.Net;

namespace ProgramAcad.Services.Modules.Compiling.Models
{
    public class CompilerResponse
    {
        public CompilerResponse(string output, HttpStatusCode statusCode, double? cpuTime)
        {
            Output = output;
            StatusCode = statusCode;
            CpuTime = cpuTime;
        }

        public string Output { get; protected set; }
        public HttpStatusCode StatusCode { get; protected set; }
        public double? CpuTime { get; protected set; }

        public bool HasCompilingError => CpuTime == null || (Output != null && Output.Contains("Traceback (most recent call last)"));
    }
}
