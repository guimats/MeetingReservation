using MeetingReservation.Application.Extensions.Mapping;
using MeetingReservation.Communication.Responses;
using MeetingReservation.Domain.Repositories.Company;
using MeetingReservation.Domain.Services.LoggedUser;
using MeetingReservation.Exceptions;
using MeetingReservation.Exceptions.ExceptionsBase;

namespace MeetingReservation.Application.UseCases.Company.GetById;

public class GetCompanyByIdUseCase : IGetCompanyByIdUseCase
{
	private readonly ILoggedUser _loggedUser;
	private readonly ICompanyReadOnlyRepository _repository;

	public GetCompanyByIdUseCase(
		ILoggedUser loggedUser, 
		ICompanyReadOnlyRepository repository)
	{
		_loggedUser = loggedUser;
		_repository = repository;
	}

	public async Task<ResponseShortCompanyJson> Execute()
	{
		var user = await _loggedUser.User();

		var company = await _repository.GetById(user.CompanyId);

		if (company is null)
			throw new NotFoundException(ResourceMessagesException.COMPANY_NOT_FOUND);

		var request = company.MapToShortRequest();

		return request;
	}
}
