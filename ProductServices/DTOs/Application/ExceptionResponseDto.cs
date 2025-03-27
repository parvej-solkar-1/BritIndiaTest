namespace ProductServices.DTOs.Application
{
    public class ExceptionResponseDto
    {
        public ExceptionResponseDto(string message, string details, int statusCode)
        {
            Message = message;
            Details = details;
            StatusCode = statusCode;
        }
        public string Message { get; set; }
        public string Details { get; set; }
        public int StatusCode { get; set; }
    }
}
