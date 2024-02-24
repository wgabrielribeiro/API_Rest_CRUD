using Microsoft.IdentityModel.Tokens;
using RestWithAspNET.API.Configurations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RestWithAspNET.API.Services.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly TokenConfigurations _configuration;

        public TokenService(TokenConfigurations configuration)
        {
            _configuration = configuration;
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret));

            var signinCredentials = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);
            var Options = new JwtSecurityToken(issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_configuration.Minutes),
                signingCredentials: signinCredentials
                );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(Options);

            return tokenString;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameter = new TokenValidationParameters 
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret)),
                ValidateLifetime = false
        };

            var tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken securityToken;

            var principal =  tokenHandler.ValidateToken(token, tokenValidationParameter, out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null &&
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCulture)) throw new SecurityTokenException("Invalid Token");


            return principal;
        }
    }
}
