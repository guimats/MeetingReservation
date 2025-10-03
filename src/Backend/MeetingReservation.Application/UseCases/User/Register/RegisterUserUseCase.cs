using FluentValidation.Results;
using MeetingReservation.Application.Extensions.Mapping;
using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;
using MeetingReservation.Domain.Repositories;
using MeetingReservation.Domain.Repositories.Token;
using MeetingReservation.Domain.Repositories.User;
using MeetingReservation.Domain.Security.Cryptography;
using MeetingReservation.Domain.Security.Tokens;
using MeetingReservation.Exceptions;
using MeetingReservation.Exceptions.ExceptionsBase;

namespace MeetingReservation.Application.UseCases.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserWriteOnlyRepository _writeRepository;
    private readonly IUserReadOnlyRepository _readRepository;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly ITokenRepository _tokenRepository;

    public RegisterUserUseCase(
        IUnitOfWork unitOfWork, 
        IUserWriteOnlyRepository writeRepository,
        IUserReadOnlyRepository readRepository,
        IAccessTokenGenerator accessTokenGenerator,
        IPasswordEncripter passwordEncripter,
        IRefreshTokenGenerator refreshTokenGenerator,
        ITokenRepository tokenRepository)
    {
        _unitOfWork = unitOfWork;
        _writeRepository = writeRepository;
        _readRepository = readRepository;
        _accessTokenGenerator = accessTokenGenerator;
        _passwordEncripter = passwordEncripter;
        _refreshTokenGenerator = refreshTokenGenerator;
        _tokenRepository = tokenRepository;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        var user = request.MapToUser();
        user.Password = _passwordEncripter.Encrypt(user.Password);
        user.UserIdentifier = Guid.NewGuid();

        await _writeRepository.Add(user);

        await _unitOfWork.Commit();

        var refreshToken = await CreateAndSaveRefreshToken(user);

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Tokens = new ResponseTokensJson
            {
                AcccessToken = _accessTokenGenerator.Generate(user.UserIdentifier),
                RefreshToken = refreshToken
            }
        };
    }

    private async Task<string> CreateAndSaveRefreshToken(Domain.Entities.User user)
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

        var existActiceEmail = await _readRepository.ExistActiveEmail(request.Email);

        if (existActiceEmail)
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceMessagesException.EMPTY_EMAIL));

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
