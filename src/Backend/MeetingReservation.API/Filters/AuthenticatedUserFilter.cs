using MeetingReservation.Communication.Responses;
using MeetingReservation.Domain.Repositories.User;
using MeetingReservation.Domain.Security.Tokens;
using MeetingReservation.Exceptions;
using MeetingReservation.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace MeetingReservation.API.Filters
{
    public class AuthenticatedUserFilter : IAsyncAuthorizationFilter
    {
        private readonly IAccessTokenValidator _accessTokenValidator;
        private readonly IUserReadOnlyRepository _repository;

        public AuthenticatedUserFilter(IAccessTokenValidator accessTokenValidator, IUserReadOnlyRepository repository)
        {
            _accessTokenValidator = accessTokenValidator;
            _repository = repository;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var token = TokenOnRequest(context);

                var userIdentifier = _accessTokenValidator.ValidateAndGetUserIdentifier(token);

                var exist = await _repository.ExistActiveUserWithIdentifier(userIdentifier);
                if (!exist)
                {
                    throw new UserNotExistException();
                }
            }
            catch (SecurityTokenExpiredException)
            {
                context.Result = new UnauthorizedObjectResult(new ResponseErrorJson("TokeIsExpired")
                {
                    TokenIsExpired = true,
                });
            }
            catch (MeetingReservationException ex)
            {
                context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ex.Message));
            }
            catch
            {
                context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ResourceMessagesException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE));
            }
        }

        private static string TokenOnRequest(AuthorizationFilterContext context)
        {
            var authentication = context.HttpContext.Request.Headers.Authorization.ToString();

            if (string.IsNullOrEmpty(authentication))
            {
                throw new NoTokenException();
            }

            // mesmo que fazer -> authentication[7..].Trim()
            // o retorno da authorization na request vai conter esse Bearer, por isso removemos
            return authentication["Bearer ".Length..].Trim();
        }
    }
}
