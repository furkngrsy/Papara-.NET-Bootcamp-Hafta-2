namespace Papara_Bootcamp.Models
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public int StatusCode { get; set; }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public new T Data { get; set; }
    }
}
