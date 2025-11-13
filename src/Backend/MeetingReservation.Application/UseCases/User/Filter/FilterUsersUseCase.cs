using MeetingReservation.Application.Extensions.Mapping;
using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;
using MeetingReservation.Domain.Repositories.User;

namespace MeetingReservation.Application.UseCases.User.Filter;

public class FilterUsersUseCase : IFilterUsersUseCase
{
	private readonly IUserReadOnlyRepository _repository;

	public FilterUsersUseCase(IUserReadOnlyRepository repository)
	{
		_repository = repository;
	}
	

	public async Task<ResponseUsersJson> Execute(RequestFilterUsersJson request)
	{
		var filter = request.MapToFilter();

		var users = await _repository.Filter(filter);

		return users.MapToUsers();
	}
}
