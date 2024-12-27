namespace Cart.Application.DTOs
{
    public class ApiResponseDTO<T>
    {
        public T? Data { get; set; }
        public string? Message { get; set; }
        public string[]? Errors { get; set; }
    }
}
