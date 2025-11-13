using MeetingReservation.Communication.Requests;

namespace MeetingReservation.Application.UseCases.Company.Register;

public interface IRegisterCompanyUseCase
{
	public Task Execute(RequestRegisterCompanyJson request);
}
