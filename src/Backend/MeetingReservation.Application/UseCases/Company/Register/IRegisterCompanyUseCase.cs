using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;

namespace MeetingReservation.Application.UseCases.Company.Register;

public interface IRegisterCompanyUseCase
{
	public Task<ResponseRegisteredUserJson> Execute(RequestRegisterCompanyJson request);
}
