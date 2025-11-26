using FluentValidation;
using MeetingReservation.Communication.Requests;
using MeetingReservation.Exceptions;

namespace MeetingReservation.Application.UseCases.Company.Update;

public class UpdateCompanyValidator : AbstractValidator<RequestUpdateCompanyJson>
{
	public UpdateCompanyValidator()
	{
		RuleFor(company => company.Name).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_NAME);
	}
}
