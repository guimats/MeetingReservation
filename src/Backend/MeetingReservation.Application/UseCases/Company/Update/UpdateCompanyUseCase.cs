using MeetingReservation.Application.Extensions.Mapping;
using MeetingReservation.Communication.Requests;
using MeetingReservation.Domain.Enums;
using MeetingReservation.Domain.Repositories;
using MeetingReservation.Domain.Repositories.Company;
using MeetingReservation.Domain.Services.LoggedUser;
using MeetingReservation.Exceptions;
using MeetingReservation.Exceptions.ExceptionsBase;

namespace MeetingReservation.Application.UseCases.Company.Update;

public class UpdateCompanyUseCase : IUpdateCompanyUseCase
{
	private readonly ICompanyUpdateOnlyRepository _repository;
	private readonly ILoggedUser _loggedUser;
	private readonly IUnitOfWork _unitOfWork;

	public UpdateCompanyUseCase(
		ICompanyUpdateOnlyRepository repository,
		ILoggedUser loggedUser,
		IUnitOfWork unitOfWork)
	{
		_repository = repository;
		_loggedUser = loggedUser;
		_unitOfWork = unitOfWork;
	}

	public async Task Execute(RequestUpdateCompanyJson request)
	{
		Validate(request);

		var user = await _loggedUser.User();

		var company = await _repository.GetById(user.CompanyId);

		if (company == null)
			throw new NotFoundException(ResourceMessagesException.COMPANY_NOT_FOUND);

		if (company.Users.Contains(user) == false && user.Role.Equals(Role.Admin) == false)
			throw new ForbiddenException();

		company = request.MapToCompany(company);

		_repository.Update(company);

		await _unitOfWork.Commit();
	}

	private static void Validate(RequestUpdateCompanyJson request)
	{
		var validator = new UpdateCompanyValidator();

		validator.Validate(request);
	}
}
