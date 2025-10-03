using MeetingReservation.Communication.Requests;
using MeetingReservation.Domain.Repositories.User;
using MeetingReservation.Domain.Security.Cryptography;

namespace MeetingReservation.Application.UseCases.Login.DoLogin;

public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _repository;
    private readonly IPasswordEncripter _passwordEncripter;

    public DoLoginUseCase(
        IUserReadOnlyRepository repository,
        IPasswordEncripter passwordEncripter)
    {
        _repository = repository;
        _passwordEncripter = passwordEncripter;
    }

    public async Task Execute(RequestLoginJson request)
    {
        var user = await _repository.GetByEmail(request.Email);

        // CONTINUAR IMPLEMENTAÇÃO
    }
}
