using MeetingReservation.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MeetingReservation.Infrastructure.Security.Tokens.Access.Generator
{
    public class JwtTokenGenerator : JwtTokenHandler, IAccessTokenGenerator
    {
        private readonly uint _expirationTimeMinutes;
        private readonly string _signingKey;

        public JwtTokenGenerator(uint expirationTimeMinutes, string signingKey)
        {
            _expirationTimeMinutes = expirationTimeMinutes;
            _signingKey = signingKey;
        }

        public string Generate(Guid userIdentifier)
        {
            // criando uma lista de claims e incluindo o userIdentifier
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Sid, userIdentifier.ToString())
            };

            // definindo a chave de assinatura e a data de expiração do token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
                SigningCredentials = new SigningCredentials(SecurityKey(_signingKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }
    }
}


