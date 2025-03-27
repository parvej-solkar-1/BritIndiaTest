using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Products.BusinessLayer.Interfaces;
using ProductServices.DTOs.Login;

namespace ProductServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ILogger<TokensController> _logger;

        public TokensController(
            IMapper mapper,
            ISessionService productService,
            ILogger<TokensController> logger)
        {
            _mapper = mapper;
            _sessionService = productService;
            _logger = logger;
        }

        [HttpPost("login")]
        //[Route("tokens")]
        public async Task<IActionResult> CreateToken(LoginRequestDto loginRequestDto)
        {
            _logger.LogTrace("START: Getting token using Token service");
            var token = _sessionService.CreateLoginToken(loginRequestDto.UserId, loginRequestDto.Password);
            _logger.LogTrace("END: Getting token using Token service");

            var tokenDto = _mapper.Map<LoginResponseDto>(token);
            return Ok(tokenDto);
        }

        [HttpPost("refresh-tokens")]
        //[Route("tokens")]
        public async Task<IActionResult> CreateRefreshToken(string token)
        {
            _logger.LogTrace("START: Creating refresh token using Token service");
            var newToken = _sessionService.CreateRefreshToken(token);
            _logger.LogTrace("END: Creating refresh token using Token service");

            var tokenDto = _mapper.Map<LoginResponseDto>(token);
            return Ok(tokenDto);
        }

        [HttpPost("refresh-tokens/revoke")]
        //[Route("tokens")]
        public async Task<IActionResult> RevokeRefreshToken()
        {
            _logger.LogTrace("START: Revoking refresh token using Token service");
            var newToken = _sessionService.RevokeRefreshTokens();
            _logger.LogTrace("END: Revoking refresh token using Token service");

            return NoContent();
        }
    }
}
