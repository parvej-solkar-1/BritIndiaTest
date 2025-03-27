using System.ComponentModel.DataAnnotations;

namespace ProductServices.DTOs.Login
{
    public class LoginRequestDto
    {
        [Required]
        public string UserId { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
    }
}
