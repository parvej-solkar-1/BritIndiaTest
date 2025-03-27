namespace Products.BusinessLayer.Models
{
    public class JWTSettings
    {
        public string SecurityKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TokenExpiresInMinutes { get; set; }
    }
}
