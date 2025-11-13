using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;

namespace MeetingReservation.Application.UseCases.User.Filter;

public interface IFilterUsersUseCase
{
	public Task<ResponseUsersJson> Execute(RequestFilterUsersJson request);
}
