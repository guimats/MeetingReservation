using MeetingReservation.Application.Extensions.Mapping;
using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;
using MeetingReservation.Domain.Repositories.Room;

namespace MeetingReservation.Application.UseCases.Room.Filter;

public class FilterRoomsUseCase : IFilterRoomsUseCase
{
	private readonly IRoomReadOnlyRepository _repository;

	public FilterRoomsUseCase(IRoomReadOnlyRepository repository)
	{
		_repository = repository;
	}

	public async Task<ResponseRoomsJson> Execute(RequestFilterRoomsJson request)
	{
		var filter = request.MapToFilter();

		var result = await _repository.Filter(filter);

		var rooms = result.MapToRooms();

		return rooms;
	}
}
