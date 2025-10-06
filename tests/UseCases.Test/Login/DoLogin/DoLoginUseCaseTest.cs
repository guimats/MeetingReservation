using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositorioes;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using MeetingReservation.Application.UseCases.Login.DoLogin;
using MeetingReservation.Communication.Requests;
using MeetingReservation.Exceptions;
using MeetingReservation.Exceptions.ExceptionsBase;
using Shouldly;

namespace UseCases.Test.Login.DoLogin;

public class DoLoginUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, var password) = UserBuilder.Build();

        var useCase = CreateUseCase(user);

        var result = await useCase.Execute(new RequestLoginJson
        {
            Email = user.Email,
            Password = password
        });

        result.ShouldNotBeNull();
        result.Tokens.ShouldNotBeNull();
        result.Name.ShouldSatisfyAllConditions(
            name => name.ShouldNotBeNullOrWhiteSpace(),
            name => name.ShouldBe(user.Name));
        result.Tokens.AccessToken.ShouldNotBeNullOrEmpty();
    }

    [Fact]
    public async Task Error_Invalid_User()
    {
        var request = RequestLoginJsonBuilder.Build();

        var useCase = CreateUseCase();

        Func<Task> act = async () => { await useCase.Execute(request); };

        var exception = await act.ShouldThrowAsync<InvalidLoginException>();

        exception.Message.Equals(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID);
    }

    private static DoLoginUseCase CreateUseCase(
        MeetingReservation.Domain.Entities.User? user = null)
    {
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var userReadOnlyRepositoryBuilder = new UserReadOnlyRepositoryBuilder();
        var accessTokenGenerator = JwtTokenGeneratorBuilder.Build();
        var refreshTokenGenerator = RefreshTokenGeneratorBuilder.Build();
        var tokenRepository = new TokenRepositoryBuilder().Build();
        var unitOfWork = UnitOfWorkBuilder.Build();

        if (user is not null)
            userReadOnlyRepositoryBuilder.GetByEmail(user);

        return new DoLoginUseCase(
            userReadOnlyRepositoryBuilder.Build(),
            passwordEncripter,
            accessTokenGenerator,
            refreshTokenGenerator,
            tokenRepository,
            unitOfWork);
    }
}
