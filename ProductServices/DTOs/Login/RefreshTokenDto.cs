namespace ProductServices.DTOs.Login
{
    public class RefreshTokenDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public bool IsActive { get; set; }
    }
}
