using Products.DataAccess.Models;

namespace Products.BusinessLayer.Interfaces
{
    public interface ISessionService
    {
        Task<LoginResponse> CreateLoginToken(string userId, string password);
        Task<string> CreateRefreshToken(string token);
        Task<bool> RevokeRefreshTokens();
    }
}
