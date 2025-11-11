using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositorioes;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using MeetingReservation.Application.UseCases.User.Register;
using MeetingReservation.Communication.Requests;
using MeetingReservation.Exceptions;
using MeetingReservation.Exceptions.ExceptionsBase;
using Shouldly;

namespace UseCases.Test.User.Register;

public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, var password) = UserBuilder.Build();

        var useCase = CreateUseCase(user);

        var result = await useCase.Execute(new RequestRegisterUserJson
        {
            Email = user.Email,
            Name = user.Name,
            Password = password,
            Role = user.Role,
        });

        result.ShouldNotBeNull();
        result.Tokens.ShouldNotBeNull();
        result.Name.ShouldSatisfyAllConditions(
            name => name.ShouldNotBeNullOrWhiteSpace(),
            name => name.ShouldBe(user.Name));
        result.Tokens.AccessToken.ShouldNotBeNullOrEmpty();
    }

    [Fact]
    public async Task Error_Invalid_Email()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = "email.invaldo";

        var useCase = CreateUseCase();

        Func<Task> act = async () => { await useCase.Execute(request); };

        var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();

        exception.Message.Equals(ResourceMessagesException.INVALID_EMAIL);
    }

    private RegisterUserUseCase CreateUseCase(MeetingReservation.Domain.Entities.User? user = null)
    {
        var unitOfWork = UnitOfWorkBuilder.Build();
        var writeRepository = UserWriteOnlyRepositoryBuilder.Build();
        var readRepositoryBuilder = new UserReadOnlyRepositoryBuilder();
        var accessTokenGenerator = JwtTokenGeneratorBuilder.Build();
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var refreshTokenGenerator = RefreshTokenGeneratorBuilder.Build();
        var tokenRepository = new TokenRepositoryBuilder().Build();

        if (user is not null)
            readRepositoryBuilder.GetByEmail(user);

        return new RegisterUserUseCase(
            unitOfWork,
            writeRepository,
            readRepositoryBuilder.Build(),
            accessTokenGenerator,
            passwordEncripter,
            refreshTokenGenerator,
            tokenRepository);
    }
}
