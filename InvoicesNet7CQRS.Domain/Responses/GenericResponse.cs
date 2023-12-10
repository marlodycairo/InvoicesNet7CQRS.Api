namespace InvoicesNet7CQRS.Domain.Responses
{
    public class GenericResponse<TResult>
    {
        public GenericResponse()
        {

        }

        public GenericResponse(string message, TResult result, object error)
        {
            Message = message;
            Result = result;
            Error = error;
        }

        public string Message { get; set; } = string.Empty;
        public TResult? Result { get; set; }
        public object? Error { get; set; }
    }
}
