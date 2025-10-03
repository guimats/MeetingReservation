using MeetingReservation.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MeetingReservation.Infrastructure.Security.Tokens.Access.Validator;

public class JwtTokenValidator : JwtTokenHandler, IAccessTokenValidator
{
    private readonly string _signingKey;

    public JwtTokenValidator(string signingKey) => _signingKey = signingKey;

    public Guid ValidateAndGetUserIdentifier(string token)
    {
        // criando parametros para a validação do token
        var validationParameter = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            // aqui recebe o signingKey formatado
            IssuerSigningKey = SecurityKey(_signingKey),
            ClockSkew = new TimeSpan(0)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        // valida o token, retornando a lista de claims
        var principal = tokenHandler.ValidateToken(token, validationParameter, out _);

        // pega o claim de userIdentifier que criamos
        var userIdentifier = principal.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

        // retorna o userIdentifier em formato Guid
        return Guid.Parse(userIdentifier);
    }
}
