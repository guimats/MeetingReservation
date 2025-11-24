using MeetingReservation.Application.Extensions.Mapping;
using MeetingReservation.Application.UseCases.Helper;
using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;
using MeetingReservation.Domain.Repositories;
using MeetingReservation.Domain.Repositories.Company;
using MeetingReservation.Domain.Repositories.User;
using MeetingReservation.Domain.Security.Tokens;

namespace MeetingReservation.Application.UseCases.Company.Register;

public class RegisterCompanyUseCase : IRegisterCompanyUseCase
{
	private readonly ICompanyWriteOnlyRepository _companyRepository;
	private readonly IUserWriteOnlyRepository _userWriteRepository;
	private readonly IRegisterUserHelper _registerUserHelper;
	private readonly IAccessTokenGenerator _accessTokenGenerator;
	private readonly IUnitOfWork _unitOfWork;

	public RegisterCompanyUseCase(
		ICompanyWriteOnlyRepository companyRepository,
		IUserWriteOnlyRepository userWriteRepository,
		IRegisterUserHelper registerUserHelper,
		IAccessTokenGenerator accessTokenGenerator,
		IUnitOfWork unitOfWork)
	{
		_companyRepository = companyRepository;
		_userWriteRepository = userWriteRepository;
		_registerUserHelper = registerUserHelper;
		_accessTokenGenerator = accessTokenGenerator;
		_unitOfWork = unitOfWork;
	}

	public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterCompanyJson request)
	{
		Validate(request);

		var userRequest = request.MapToRegisterUser();

		var user = await _registerUserHelper.CreateUser(userRequest);

		var company = request.MapToCompany();

		await _userWriteRepository.Add(user);

		await _companyRepository.Add(company);

		await _unitOfWork.Commit();

		var refreshToken = await _registerUserHelper.CreateAndSaveRefreshToken(user);

		return new ResponseRegisteredUserJson
		{
			Name = user.Name,
			Tokens = new ResponseTokensJson
			{
				AccessToken = _accessTokenGenerator.Generate(user.UserIdentifier, user.Role),
				RefreshToken = refreshToken
			}
		};
	}

	private void Validate(RequestRegisterCompanyJson request)
	{
		var validator = new RegisterCompanyValidator();

		validator.Validate(request);
	}
}
