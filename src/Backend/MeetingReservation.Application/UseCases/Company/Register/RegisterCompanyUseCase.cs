using MeetingReservation.Application.Extensions.Mapping;
using MeetingReservation.Communication.Requests;
using MeetingReservation.Domain.Repositories;
using MeetingReservation.Domain.Repositories.Company;
using MeetingReservation.Domain.Repositories.User;

namespace MeetingReservation.Application.UseCases.Company.Register;

public class RegisterCompanyUseCase : IRegisterCompanyUseCase
{
	private readonly ICompanyWriteOnlyRepository _companyRepository;
	private readonly IUserWriteOnlyRepository _userRepository;
	private readonly IUnitOfWork _unitOfWork;

	public RegisterCompanyUseCase(
		ICompanyWriteOnlyRepository companyRepository,
		IUserWriteOnlyRepository userRepository,
		IUnitOfWork unitOfWork)
	{
		_companyRepository = companyRepository;
		_userRepository = userRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task Execute(RequestRegisterCompanyJson request)
	{
		Validate(request);

		var company = request.MapToCompany();

		await _companyRepository.Add(company);
		//	TODO - INCOMPLETO E NECESSITA DE ALTERAÇÃO
		var user = request.MapToUser();
		user.CompanyId = company.Id;

		await _userRepository.Add(user);
	}

	private void Validate(RequestRegisterCompanyJson request)
	{
		throw new NotImplementedException();
	}
}
