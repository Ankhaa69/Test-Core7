namespace ItemManagment.Helpers
{
    public class Response<T>
    {
        public Response()
        {
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
        }
        public Response(T data)
        {
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
            Data = data;
        }
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string[]? Errors { get; set; }
        public string? Message { get; set; }
    }
}
