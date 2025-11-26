using MeetingReservation.Application.Extensions.Mapping;
using MeetingReservation.Communication.Requests;
using MeetingReservation.Domain.Repositories;
using MeetingReservation.Domain.Repositories.Room;
using MeetingReservation.Domain.Services.LoggedUser;
using MeetingReservation.Exceptions.ExceptionsBase;

namespace MeetingReservation.Application.UseCases.Room.Register;

public class RegisterRoomUseCase : IRegisterRoomUseCase
{
	private readonly IRoomWriteOnlyRepository _repository;
	private readonly ILoggedUser _loggedUser;
	private readonly IUnitOfWork _unitOfWork;

	public RegisterRoomUseCase(
		IRoomWriteOnlyRepository repository,
		ILoggedUser loggedUser,
		IUnitOfWork unitOfWork)
	{
		_repository = repository;
		_loggedUser = loggedUser;
		_unitOfWork = unitOfWork;
	}

	public async Task Execute(RequestRoomJson request)
	{
		Validate(request);

		var user = await _loggedUser.User();
		request.UserId = user.Id;

		var room = request.MapToRoom();

		room.CompanyId = user.CompanyId;

		await _repository.Add(room);

		await _unitOfWork.Commit();
	}

	private void Validate(RequestRoomJson request)
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
