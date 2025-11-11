using Shouldly;
using CommonTestUtilities.Requests;
using MeetingReservation.Application.UseCases.User.Register;
using MeetingReservation.Exceptions;
using MeetingReservation.Domain.Enums;

namespace Validators.Test.User.Register;

public class RegisterUserValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Error_Name_Empty()
    {
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(
            errors => errors.ShouldHaveSingleItem(),
            errors => errors.ShouldContain(e => e.ErrorMessage == ResourceMessagesException.EMPTY_NAME));
    }

    [Fact]
    public void Error_Email_Empty()
    {
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(
            errors => errors.ShouldHaveSingleItem(),
            errors => errors.ShouldContain(e => e.ErrorMessage == ResourceMessagesException.EMPTY_EMAIL));
    }

    [Fact]
    public void Error_Password_Empty()
    {
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Password = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(
            errors => errors.ShouldHaveSingleItem(),
            errors => errors.ShouldContain(e => e.ErrorMessage == ResourceMessagesException.EMPTY_PASSWORD));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Error_Password_Invalid(int passwordLength)
    {
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build(passwordLength);

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(
            errors => errors.ShouldHaveSingleItem(),
            errors => errors.ShouldContain(e => e.ErrorMessage == ResourceMessagesException.PASSWORD_LONGER_THAN_SIX));
    }

    [Fact]
    public void Error_Invalid_Role()
    {
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Role = (Role)1000;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(
            errors => errors.ShouldHaveSingleItem(),
            errors => errors.ShouldContain(e => e.ErrorMessage == ResourceMessagesException.ROLE_NOT_SUPPORTED));
    }
}
