namespace ProductServices.DTOs.Application
{
    public class ExceptionResponseDto
    {
        public ExceptionResponseDto(string message, string details)
        {
            Message = message;
            Details = details;
        }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}
