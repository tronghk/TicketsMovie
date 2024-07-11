using ZingMp3API.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ZingMp3API.Data;

namespace ZingMp3API.Service.Imployment
{
    public class TokenServices : IToken
    {

        private readonly UserManager<AccountIdentity> _userManager;
        private readonly IConfiguration _configuration;
        public TokenServices(UserManager<AccountIdentity> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<ResponseModel> CheckToken(TokenModel tokenModel)
        {
            if (tokenModel is null)
            {
                return new ResponseModel
                {
                    Status = 401,
                    Message = "token is null"
                };
            }
            string? accessToken = tokenModel.AccessToken;
            string? refreshToken = tokenModel.RefreshToken;
            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return new ResponseModel
                {
                    Status = 401,
                    Message = "Invalid access token or refresh token"
                };

            }
            string username = principal.Identity!.Name!;
            var user = await _userManager.FindByEmailAsync(username!);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return new ResponseModel
                {
                    Status = 401,
                    Message = "Invalid access token or refresh token"
                };
            }
            return new ResponseModel
            {
                Status = 200,
                Message = "Create token"
            };
        }

        public async Task<TokenModel> GenerareTokenModel(SignInModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,model.Email),
                new Claim(ClaimTypes.Name,model.Email),
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            var userRole = await _userManager.GetRolesAsync(user!);
            foreach (var role in userRole)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            var token = GeneratorToken(claims);
            var refreshToken = GenerateRefreshToken();

            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
            user!.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);
            await _userManager.UpdateAsync(user);
            var result = new TokenModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                Expiration = token.ValidTo
            };

            return result;
        }
        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public JwtSecurityToken GeneratorToken(List<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }

        public async Task<TokenModel> RefreshToken(ClaimsPrincipal principal)
        {
            var newAccessToken = GeneratorToken(principal.Claims.ToList());
            var newRefreshToken = GenerateRefreshToken();
            string username = principal.Identity!.Name!;
            var user = await _userManager.FindByEmailAsync(username);
            user!.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return new TokenModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken,
                Expiration = newAccessToken.ValidTo
            };
        }

        public async Task<ResponseModel> RevokeAllToken()
        {
            var list = _userManager.Users.ToList();
            foreach (var user in list)
            {
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
            }
            return new ResponseModel
            {
                Status = 200,
                Message = "Refresh all refresh account success"
            };
         }

        public async Task<ResponseModel> RevokeToken(string username)
        {
            var user = await _userManager.FindByEmailAsync(username);
            if (user == null)
            {
                return new ResponseModel
                {
                    Status = 401,
                    Message = "Not Found Account"
                };
            }
            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);
            return new ResponseModel
            {
                Status = 200,   
                Message = "Refresh token success"
            };
        }

        public async Task<TokenModel> CreateToken(List<Claim> authClaims, string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            var token = GeneratorToken(authClaims);
            var refreshToken = GenerateRefreshToken();
            user!.RefreshToken = refreshToken;
            await _userManager.UpdateAsync(user);

            var result = new TokenModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                Expiration = token.ValidTo
            };

            return result;
        }
    }
}
