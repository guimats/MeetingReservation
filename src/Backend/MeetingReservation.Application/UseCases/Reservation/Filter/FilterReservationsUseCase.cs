using MeetingReservation.Application.Extensions.Mapping;
using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;
using MeetingReservation.Domain.Repositories.Reservation;

namespace MeetingReservation.Application.UseCases.Reservation.Filter;

public class FilterReservationsUseCase : IFilterReservationsUseCase
{
	private readonly IReservationReadOnlyRepository _repository;

	public FilterReservationsUseCase(IReservationReadOnlyRepository repository)
	{
		_repository = repository;
	}

	public async Task<ResponseReservationsJson> Execute(RequestFilterReservationsJson request)
	{
		var filterDTO = request.MapToFilter();

		var reservations = await _repository.Filter(filterDTO);

		var response = reservations.MapToReservationsResponse();

		return response;
	}
}
