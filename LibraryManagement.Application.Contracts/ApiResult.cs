namespace LibraryManagement.Application.Contracts
{
    public class ApiResult<T>
    {
        public bool Success { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}