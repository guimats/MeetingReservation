
using MeetingReservation.Domain.Enums;
using MeetingReservation.Domain.Repositories;
using MeetingReservation.Domain.Repositories.User;
using MeetingReservation.Domain.Services.LoggedUser;
using MeetingReservation.Exceptions;
using MeetingReservation.Exceptions.ExceptionsBase;

namespace MeetingReservation.Application.UseCases.User.Delete;

public class DeleteUserUseCase : IDeleteUserUseCase
{
    private readonly IUserWriteOnlyRepository _writeRepository;
    private readonly IUserReadOnlyRepository _readRepository;
    private readonly ILoggedUser _loggedUser;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserUseCase(
        IUserWriteOnlyRepository writeRepository,
        IUserReadOnlyRepository readRepository,
        ILoggedUser loggedUser,
        IUnitOfWork unitOfWork)
    {
        _writeRepository = writeRepository;
        _readRepository = readRepository;
        _loggedUser = loggedUser;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(long id)
    {
        var loggedUser = await _loggedUser.User();

		if (loggedUser.Role.Equals(Role.Admin) == false)
			throw new ForbiddenException();

        var user = await _readRepository.GetById(id);

		if (user is null)
            throw new NotFoundException(ResourceMessagesException.USER_NOT_FOUND);

        await _writeRepository.Delete(id);

        await _unitOfWork.Commit();
    }
}
