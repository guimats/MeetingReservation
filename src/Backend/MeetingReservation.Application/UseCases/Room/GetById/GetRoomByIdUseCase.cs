using MeetingReservation.Application.Extensions.Mapping;
using MeetingReservation.Communication.Responses;
using MeetingReservation.Domain.Repositories.Room;
using MeetingReservation.Exceptions;
using MeetingReservation.Exceptions.ExceptionsBase;

namespace MeetingReservation.Application.UseCases.Room.GetById;

public class GetRoomByIdUseCase : IGetRoomByIdUseCase
{
	private readonly IRoomReadOnlyRepository _repository;

	public GetRoomByIdUseCase(IRoomReadOnlyRepository repository)
	{
		_repository = repository;
	}

	public async Task<ResponseRoomJson> Execute(long roomId)
	{
		var room = await _repository.GetById(roomId);
		
		if (room is null)
			throw new NotFoundException(ResourceMessagesException.ROOM_NOT_FOUND);
		
		var response = room.MapToResponse();

		return response;
	}
}
