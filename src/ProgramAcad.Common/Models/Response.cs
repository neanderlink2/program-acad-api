namespace ProgramAcad.Common.Models
{
    public class Response<TData, TError>
    {
        public Response(bool success, TData data, TError errors)
        {
            Success = success;
            Data = data;
            Errors = errors;
        }

        public bool Success { get; protected set; }
        public TData Data { get; protected set; }
        public TError Errors { get; protected set; }
    }
}
