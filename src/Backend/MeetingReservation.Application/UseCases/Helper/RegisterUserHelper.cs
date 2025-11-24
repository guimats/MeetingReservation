using FluentValidation.Results;
using MeetingReservation.Application.Extensions.Mapping;
using MeetingReservation.Application.UseCases.User.Register;
using MeetingReservation.Communication.Requests;
using MeetingReservation.Domain.Repositories;
using MeetingReservation.Domain.Repositories.Token;
using MeetingReservation.Domain.Repositories.User;
using MeetingReservation.Domain.Security.Cryptography;
using MeetingReservation.Domain.Security.Tokens;
using MeetingReservation.Exceptions;
using MeetingReservation.Exceptions.ExceptionsBase;

namespace MeetingReservation.Application.UseCases.Helper;

public class RegisterUserHelper : IRegisterUserHelper
{
	private readonly IUserReadOnlyRepository _userReadOnlyRepository;
	private readonly IPasswordEncripter _passwordEncripter;
	private readonly IRefreshTokenGenerator _refreshTokenGenerator;
	private readonly ITokenRepository _tokenRepository;
	private readonly IUnitOfWork _unitOfWork;

	public RegisterUserHelper(IUserReadOnlyRepository userReadOnlyRepository,
		IPasswordEncripter passwordEncripter,
		IRefreshTokenGenerator refreshTokenGenerator,
		ITokenRepository tokenRepository,
		IUnitOfWork unitOfWork)
	{
		_userReadOnlyRepository = userReadOnlyRepository;
		_passwordEncripter = passwordEncripter;
		_refreshTokenGenerator = refreshTokenGenerator;
		_tokenRepository = tokenRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task<Domain.Entities.User> CreateUser(RequestRegisterUserJson request) 
	{
		await Validate(request);

		var user = request.MapToUser();
		user.Password = _passwordEncripter.Encrypt(user.Password);
		user.UserIdentifier = Guid.NewGuid();

		return user;
	}

	public async Task<string> CreateAndSaveRefreshToken(Domain.Entities.User user)
	{
		var refreshToken = new Domain.Entities.RefreshToken
		{
			Value = _refreshTokenGenerator.Generate(),
			UserId = user.Id
		};

		await _tokenRepository.SaveNewRefreshToken(refreshToken);

		await _unitOfWork.Commit();

		return refreshToken.Value;
	}

	private async Task Validate(RequestRegisterUserJson request)
	{
		var validator = new RegisterUserValidator();

		var result = validator.Validate(request);

		var existActiceEmail = await _userReadOnlyRepository.ExistActiveEmail(request.Email);

		if (existActiceEmail)
			result.Errors.Add(new ValidationFailure(string.Empty, ResourceMessagesException.EMAIL_ALREADY_REGISTERED));

		if (!result.IsValid)
		{
			var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

			throw new ErrorOnValidationException(errorMessages);
		}
	}
}
