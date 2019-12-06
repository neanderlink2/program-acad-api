using ProgramAcad.Common.Models;
using System;
using System.Net;

namespace ProgramAcad.Domain.Exceptions
{
    public class CompilingFailedException : Exception
    {
        public CompilingFailedException(ExpectedError response)
        {
            Response = response;
        }

        public CompilingFailedException(string error, HttpStatusCode statusCode)
        {
            Response = new ExpectedError(statusCode, "Compiling Error", error);
        }

        public ExpectedError Response { get; private set; }

        public CompilingFailedException()
        {
        }

        public CompilingFailedException(string message) : base(message)
        {
        }

        public CompilingFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
