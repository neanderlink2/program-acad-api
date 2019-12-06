using System.Collections.Generic;

namespace ProgramAcad.Common.Models
{
    public class Response<TData>
    {
        public Response(bool success, TData data, IEnumerable<ExpectedError> errors)
        {
            Success = success;
            Data = data;
            Errors = errors;
        }

        public bool Success { get; protected set; }
        public TData Data { get; protected set; }
        public IEnumerable<ExpectedError> Errors { get; protected set; }
    }
}
