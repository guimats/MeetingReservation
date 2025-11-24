using FluentValidation;
using MeetingReservation.Application.UseCases.User.Register;
using MeetingReservation.Communication.Requests;
using MeetingReservation.Exceptions;

namespace MeetingReservation.Application.UseCases.Company.Register;

public class RegisterCompanyValidator : AbstractValidator<RequestRegisterCompanyJson>
{
	public RegisterCompanyValidator()
	{
		RuleFor(company => company.Name).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_NAME);
	}
}
