using FluentValidation;
using MeetingReservation.Communication.Requests;
using MeetingReservation.Exceptions;

namespace MeetingReservation.Application.UseCases.User.Update;

public class UpdateUserValidator : AbstractValidator<RequestUpdateUserJson>
{
    public UpdateUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_NAME);
        RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_EMAIL);
        RuleFor(user => user.Role).IsInEnum().WithMessage(ResourceMessagesException.ROLE_NOT_SUPPORTED);

        When(user => !string.IsNullOrEmpty(user.Email), () =>
        {
            RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceMessagesException.INVALID_EMAIL);
        });
    }
}
