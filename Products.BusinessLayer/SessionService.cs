using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Products.BusinessLayer.Interfaces;
using Products.BusinessLayer.Models;
using Products.BusinessLayer.Util;
using Products.DataAccess;
using Products.DataAccess.Entities;
using Products.DataAccess.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Products.BusinessLayer
{
    public class SessionService : ISessionService
    {
        private readonly ProductDbContext _context;
        private readonly JWTSettings _jwtSettings;
        private readonly PasswordConfig _passwordConfig;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(ProductDbContext context, IOptions<JWTSettings> options, IOptions<PasswordConfig> passwordOptions)
        {
            _context = context;
            _jwtSettings = options.Value;
            _passwordConfig = passwordOptions.Value;
            //_httpContextAccessor = httpContextAccessor;
        }

        public async Task<LoginResponse> CreateLoginToken(string userId, string password)
        {
            // This should be separate service and repository for users
            var user = await _context.Users.FindAsync(userId);
            if (user == null || !string.Equals(PasswordUtil.HashPassword(password, _passwordConfig.Salt), user.Password))
                throw new UnauthorizedAccessException();

            var securityKey = _jwtSettings.SecurityKey; // _configuration.GetValue<string>("JwtSettings:SecurityKey");
            var tokenExpiresInMinutes = _jwtSettings.TokenExpiresInMinutes; // _configuration.GetValue<int>("JwtSettings:TokenExpiresInMinutes");
            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.UTF8.GetBytes(securityKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    [
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iss, _jwtSettings.Issuer),
                        new Claim(JwtRegisteredClaimNames.Aud, _jwtSettings.Audience),
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserId),
                        new Claim(JwtRegisteredClaimNames.Name, user.UserId),
                        new Claim(ClaimTypes.Role.ToString(), user.Role.ToString()),
                    ]
                ),
                Expires = DateTime.UtcNow.AddMinutes(tokenExpiresInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenhandler.CreateToken(tokenDescriptor);
            string finaltoken = tokenhandler.WriteToken(token);

            var loginResponse = new LoginResponse();
            loginResponse.Token = finaltoken;
            loginResponse.ExpiresAt = tokenDescriptor.Expires.Value;
            loginResponse.RefreshToken = GenerateRefreshToken();

            var refreshTokenEntity = new RefreshToken();
            refreshTokenEntity.UserId = user.UserId;
            refreshTokenEntity.Token = loginResponse.RefreshToken;
            refreshTokenEntity.IsActive = true;
            refreshTokenEntity.ExpiresAt = DateTime.UtcNow.AddDays(7);
            _context.RefreshTokens.Add(refreshTokenEntity);
            await _context.SaveChangesAsync();

            return loginResponse;
        }

        public async Task<string> CreateRefreshToken(string token)
        {
            var refreshToken = await _context.RefreshTokens
                .Where(x => x.Token == token && x.IsActive).FirstOrDefaultAsync();

            if (refreshToken == null || refreshToken.ExpiresAt < DateTime.UtcNow)
                throw new ApplicationException("The refresh token has expired.");

            refreshToken.Token = GenerateRefreshToken();
            await _context.SaveChangesAsync();
            return refreshToken.Token;
        }

        public async Task<bool> RevokeRefreshTokens()
        {
            await _context.RefreshTokens
                // TODO: Only admin role users should be able to revoke all tokens
                //.Where(x => x.UserId == "admin")
                .ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
            return true;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
