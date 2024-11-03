namespace YanKoltukBackend.Application.Results
{
    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public static ServiceResult<T> SuccessResult(T data, string message = "")
        {
            return new ServiceResult<T> { Success = true, Data = data, Message = message };
        }

        public static ServiceResult<T> ErrorResult(string message)
        {
            return new ServiceResult<T> { Success = false, Message = message };
        }
    }
}
