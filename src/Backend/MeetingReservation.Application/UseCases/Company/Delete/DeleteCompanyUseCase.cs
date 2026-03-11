using MeetingReservation.Domain.Enums;
using MeetingReservation.Domain.Repositories;
using MeetingReservation.Domain.Repositories.Company;
using MeetingReservation.Domain.Services.LoggedUser;
using MeetingReservation.Exceptions;
using MeetingReservation.Exceptions.ExceptionsBase;

namespace MeetingReservation.Application.UseCases.Company.Delete;

public class DeleteCompanyUseCase : IDeleteCompanyUseCase
{
	private readonly ICompanyWriteOnlyRepository _repository;
	private readonly ICompanyReadOnlyRepository _readRepository;
	private readonly ILoggedUser _loggedUser;
	private readonly IUnitOfWork _unitOfWork;

	public DeleteCompanyUseCase(
		ICompanyWriteOnlyRepository repository,
		ICompanyReadOnlyRepository readRepository,
		ILoggedUser loggedUser,
		IUnitOfWork unitOfWork)
	{
		_repository = repository;
		_readRepository = readRepository;
		_loggedUser = loggedUser;
		_unitOfWork = unitOfWork;
	}

	public async Task Execute()
	{
		var user = await _loggedUser.User();

		var company = await _readRepository.GetById(user.CompanyId);

		if (company is null)
			throw new NotFoundException(ResourceMessagesException.RESERVATION_NOT_FOUND);

		if (company.Users.Contains(user) == false && user.Role.Equals(Role.Admin) == false)
			throw new ForbiddenException();

		await _repository.Delete(company.Id);

		await _unitOfWork.Commit();
	}
}
