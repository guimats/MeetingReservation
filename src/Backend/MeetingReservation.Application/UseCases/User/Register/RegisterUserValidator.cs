using FluentValidation;
using MeetingReservation.Communication.Requests;
using MeetingReservation.Exceptions;

namespace MeetingReservation.Application.UseCases.User.Register;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_NAME);
        RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_EMAIL);
        RuleFor(user => user.Password).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_PASSWORD);

        When(user => !string.IsNullOrEmpty(user.Password), () =>
        {
            RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMessagesException.PASSWORD_LONGER_THAN_SIX);
        });

        When(user => !string.IsNullOrEmpty(user.Email), () =>
        {
            RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceMessagesException.INVALID_EMAIL);
        });
    }
}
