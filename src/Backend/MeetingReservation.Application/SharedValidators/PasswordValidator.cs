using FluentValidation;
using FluentValidation.Validators;
using MeetingReservation.Exceptions;

namespace MeetingReservation.Application.SharedValidators;

public class PasswordValidator<T> : PropertyValidator<T, string>
{
    public override bool IsValid(ValidationContext<T> context, string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            context.MessageFormatter.AppendArgument("ErrorMessage", ResourceMessagesException.EMPTY_PASSWORD);

            return false;
        }

        if (password.Length < 6)
        {
            context.MessageFormatter.AppendArgument("ErrorMessage", ResourceMessagesException.PASSWORD_LONGER_THAN_SIX);

            return false;
        }

        return true;
    }

    public override string Name => "PasswordValidator";

    // pegando valor do erro e retornando
    protected override string GetDefaultMessageTemplate(string errorCode) => "{ErrorMessage}";
}
