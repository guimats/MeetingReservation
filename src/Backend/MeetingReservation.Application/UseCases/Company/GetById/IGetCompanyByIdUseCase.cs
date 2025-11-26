using MeetingReservation.Communication.Responses;

namespace MeetingReservation.Application.UseCases.Company.GetById;

public interface IGetCompanyByIdUseCase
{
	public Task<ResponseShortCompanyJson> Execute();
}
