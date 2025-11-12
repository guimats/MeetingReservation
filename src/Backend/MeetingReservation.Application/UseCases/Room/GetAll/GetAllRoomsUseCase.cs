using MeetingReservation.Application.Extensions.Mapping;
using MeetingReservation.Communication.Responses;
using MeetingReservation.Domain.Repositories.Room;

namespace MeetingReservation.Application.UseCases.Room.GetAll;

public class GetAllRoomsUseCase : IGetAllRoomsUseCase
{
	private readonly IRoomReadOnlyRepository _repository;

	public GetAllRoomsUseCase(IRoomReadOnlyRepository repository)
	{
		_repository = repository;
	}

	public async Task<ResponseAllRoomsJson> Execute()
	{
		var result = await _repository.GetAllRooms();

		var rooms = result.MapToAllRooms();

		return rooms;
	}
}
