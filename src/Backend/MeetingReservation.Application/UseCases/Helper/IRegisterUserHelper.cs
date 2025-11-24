using MeetingReservation.Communication.Requests;

namespace MeetingReservation.Application.UseCases.Helper;

public interface IRegisterUserHelper
{
	public Task<Domain.Entities.User> CreateUser(RequestRegisterUserJson request);

	public Task<string> CreateAndSaveRefreshToken(Domain.Entities.User user);
}
