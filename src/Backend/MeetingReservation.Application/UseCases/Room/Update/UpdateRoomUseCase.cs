using MeetingReservation.Application.Extensions.Mapping;
using MeetingReservation.Communication.Requests;
using MeetingReservation.Domain.Repositories;
using MeetingReservation.Domain.Repositories.Room;
using MeetingReservation.Domain.Services.LoggedUser;
using MeetingReservation.Exceptions;
using MeetingReservation.Exceptions.ExceptionsBase;

namespace MeetingReservation.Application.UseCases.Room.Update;

public class UpdateRoomUseCase : IUpdateRoomUseCase
{
	private readonly IRoomUpdateOnlyRepository _repository;
	private readonly ILoggedUser _loggedUser;
	private readonly IUnitOfWork _unitOfWork;

	public UpdateRoomUseCase(
		IRoomUpdateOnlyRepository repository,
		ILoggedUser loggedUser,
		IUnitOfWork unitOfWork)
	{
		_repository = repository;
		_loggedUser = loggedUser;
		_unitOfWork = unitOfWork;
	}

	public async Task Execute(RequestRoomJson request, long roomId)
	{
		Validate(request);

		var user = await _loggedUser.User();

		var room = await _repository.GetByID(roomId, user.Id);

		if (room is null)
			throw new NotFoundException(ResourceMessagesException.ROOM_NOT_FOUND);

		room = request.MapToRoom(room);

		_repository.Update(room);

		await _unitOfWork.Commit();
	}

	private static void Validate(RequestRoomJson request)
	{
		var validator = new RoomValidator();

		var result = validator.Validate(request);

		if (!result.IsValid)
		{
			var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

			throw new ErrorOnValidationException(errorMessages);
		}
	}
}
