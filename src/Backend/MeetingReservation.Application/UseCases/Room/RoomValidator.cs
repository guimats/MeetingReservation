using FluentValidation;
using MeetingReservation.Communication.Requests;
using MeetingReservation.Exceptions;

namespace MeetingReservation.Application.UseCases.Room;

public class RoomValidator : AbstractValidator<RequestRoomJson>
{
	public RoomValidator()
	{
		RuleFor(room => room.Name).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_NAME);
		RuleFor(room => room.Capacity).NotEmpty().WithMessage(ResourceMessagesException.EMPTY_CAPACITY);
	}


}
