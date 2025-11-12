
using MeetingReservation.Domain.Repositories;
using MeetingReservation.Domain.Repositories.Room;
using MeetingReservation.Domain.Services.LoggedUser;

namespace MeetingReservation.Application.UseCases.Room.Delete;

public class DeleteRoomUseCase : IDeleteRoomUseCase
{
	private readonly IRoomWriteOnlyRepository _repository;
	private readonly ILoggedUser _loggedUser;
	private readonly IUnitOfWork _unitOfWork;

	public DeleteRoomUseCase(
		IRoomWriteOnlyRepository repository,
		ILoggedUser loggedUser,
		IUnitOfWork unitOfWork)
	{
		_repository = repository;
		_loggedUser = loggedUser;
		_unitOfWork = unitOfWork;
	}

	public async Task Execute(long roomId)
	{
		var user = await _loggedUser.User();

		await _repository.Delete(roomId, user.Id);

		await _unitOfWork.Commit();
	}
}
