using FluentValidation;
using MeetingReservation.Communication.Requests;
using MeetingReservation.Exceptions;

namespace MeetingReservation.Application.UseCases.Reservation;

public class ReservationValidator : AbstractValidator<RequestReservationJson>
{
    public ReservationValidator()
    {
        RuleFor(res => res.Name).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_NAME);
        RuleFor(res => res.Description.Length).LessThan(500).WithMessage(ResourceMessagesException.DESCRIPTION_TOO_LONGER);
        RuleFor(res => res.InitialTime).LessThan(res => res.EndTime).WithMessage(ResourceMessagesException.INITIAL_TIME_MUST_BE_EARLIER);
        RuleFor(res => res.Participants).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_PARTICIPANTS);
	}
}
