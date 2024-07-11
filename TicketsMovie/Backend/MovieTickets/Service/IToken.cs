using ZingMp3API.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ZingMp3API.Service
{
    public interface IToken
    {
        public Task<TokenModel> GenerareTokenModel(SignInModel model);
        public Task<ResponseModel> CheckToken(TokenModel token);
        public Task<TokenModel> RefreshToken(ClaimsPrincipal token);
        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
        public Task<TokenModel> CreateToken(List<Claim> authClaims, string email);
        public JwtSecurityToken GeneratorToken(List<Claim> claims);

        public Task<ResponseModel> RevokeToken(string username);

        public Task<ResponseModel> RevokeAllToken();

      //  public Task<TokenModel> CreateToken(List<Claim> authClaims, string email);
    }
}
