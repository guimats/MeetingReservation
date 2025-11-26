using MeetingReservation.Communication.Requests;

namespace MeetingReservation.Application.UseCases.Company.Update;

public interface IUpdateCompanyUseCase
{
	public Task Execute(RequestUpdateCompanyJson request);
}
