using FluentValidation.Results;
using MeetingReservation.Application.Extensions.Mapping;
using MeetingReservation.Communication.Requests;
using MeetingReservation.Domain.Repositories;
using MeetingReservation.Domain.Repositories.User;
using MeetingReservation.Domain.Services.LoggedUser;
using MeetingReservation.Exceptions;
using MeetingReservation.Exceptions.ExceptionsBase;

namespace MeetingReservation.Application.UseCases.User.Update;

public class UpdateUserUseCase : IUpdateUserUseCase
{
    private readonly IUserUpdateOnlyRepository _updateRepository;
    private readonly IUserReadOnlyRepository _readRepository;
    private readonly ILoggedUser _loggedUser;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserUseCase(
        IUserUpdateOnlyRepository updateRepository, 
        ILoggedUser loggedUser, 
        IUnitOfWork unitOfWork, 
        IUserReadOnlyRepository readRepository)
    {
        _updateRepository = updateRepository;
        _loggedUser = loggedUser;
        _unitOfWork = unitOfWork;
        _readRepository = readRepository;
    }

    public async Task Execute(RequestUpdateUserJson request)
    {
        await Validate(request);

        var loggedUser = await _loggedUser.User();

        loggedUser = request.MapToUser(loggedUser);

        _updateRepository.Update(loggedUser);

        await _unitOfWork.Commit();
	}

    private async Task Validate(RequestUpdateUserJson request)
    {
        var validator = new UpdateUserValidator();

        var result = validator.Validate(request);

        var existActiceEmail = await _readRepository.ExistActiveEmail(request.Email);

        if (existActiceEmail)
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceMessagesException.EMAIL_ALREADY_REGISTERED));

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
