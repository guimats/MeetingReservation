using MeetingReservation.Application.UseCases.Helper;
using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;
using MeetingReservation.Domain.Repositories;
using MeetingReservation.Domain.Repositories.User;
using MeetingReservation.Domain.Security.Tokens;

namespace MeetingReservation.Application.UseCases.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserWriteOnlyRepository _writeRepository;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IRegisterUserHelper _registerUserHelper;

    public RegisterUserUseCase(
        IUnitOfWork unitOfWork, 
        IUserWriteOnlyRepository writeRepository,
        IAccessTokenGenerator accessTokenGenerator,
        IRegisterUserHelper registerUserHelper)
    {
        _unitOfWork = unitOfWork;
        _writeRepository = writeRepository;
        _accessTokenGenerator = accessTokenGenerator;
        _registerUserHelper = registerUserHelper;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        var user = await _registerUserHelper.CreateUser(request);

        await _writeRepository.Add(user);

        await _unitOfWork.Commit();

        var refreshToken = await _registerUserHelper.CreateAndSaveRefreshToken(user);

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Tokens = new ResponseTokensJson
            {
                AccessToken = _accessTokenGenerator.Generate(user),
                RefreshToken = refreshToken
            }
        };
    }
}
