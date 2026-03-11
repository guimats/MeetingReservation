using MeetingReservation.Application.UseCases.Helper;
using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;
using MeetingReservation.Domain.Entities;
using MeetingReservation.Domain.Enums;
using MeetingReservation.Domain.Repositories;
using MeetingReservation.Domain.Repositories.User;
using MeetingReservation.Domain.Security.Tokens;
using MeetingReservation.Domain.Services.LoggedUser;
using MeetingReservation.Exceptions.ExceptionsBase;

namespace MeetingReservation.Application.UseCases.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserWriteOnlyRepository _writeRepository;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IRegisterUserHelper _registerUserHelper;
    private readonly ILoggedUser _loggedUser;

    public RegisterUserUseCase(
        IUnitOfWork unitOfWork, 
        IUserWriteOnlyRepository writeRepository,
        IAccessTokenGenerator accessTokenGenerator,
        IRegisterUserHelper registerUserHelper,
        ILoggedUser loggedUser)
    {
        _unitOfWork = unitOfWork;
        _writeRepository = writeRepository;
        _accessTokenGenerator = accessTokenGenerator;
        _registerUserHelper = registerUserHelper;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        var loggedUser = await _loggedUser.User();

		if (loggedUser.Role.Equals(Role.Admin) == false)
			throw new ForbiddenException();

		var newUser = await _registerUserHelper.CreateUser(request);

        await _writeRepository.Add(newUser);

        await _unitOfWork.Commit();

        var refreshToken = await _registerUserHelper.CreateAndSaveRefreshToken(newUser);

        return new ResponseRegisteredUserJson
        {
            Name = newUser.Name,
            Tokens = new ResponseTokensJson
            {
                AccessToken = _accessTokenGenerator.Generate(newUser),
                RefreshToken = refreshToken
            }
        };
    }
}
