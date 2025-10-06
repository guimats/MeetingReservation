
using MeetingReservation.Domain.Repositories;
using MeetingReservation.Domain.Repositories.User;
using MeetingReservation.Exceptions;
using MeetingReservation.Exceptions.ExceptionsBase;

namespace MeetingReservation.Application.UseCases.User.Delete;

public class DeleteUserUseCase : IDeleteUserUseCase
{
    private readonly IUserWriteOnlyRepository _writeRepository;
    private readonly IUserReadOnlyRepository _readRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserUseCase(
        IUserWriteOnlyRepository writeRepository,
        IUserReadOnlyRepository readRepository,
        IUnitOfWork unitOfWork)
    {
        _writeRepository = writeRepository;
        _readRepository = readRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(long id)
    {
        var user = await _readRepository.GetById(id);

        if (user is null)
            throw new NotFoundException(ResourceMessagesException.USER_NOT_FOUND);

        await _writeRepository.Delete(id);

        await _unitOfWork.Commit();
    }
}
