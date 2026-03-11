
using MeetingReservation.Domain.Enums;
using MeetingReservation.Domain.Repositories;
using MeetingReservation.Domain.Repositories.Reservation;
using MeetingReservation.Domain.Services.LoggedUser;
using MeetingReservation.Exceptions;
using MeetingReservation.Exceptions.ExceptionsBase;

namespace MeetingReservation.Application.UseCases.Reservation.Delete;

public class DeleteReservationUseCase : IDeleteReservationUseCase
{
    private readonly IReservationWriteOnlyRepository _writeRepository;
    private readonly IReservationReadOnlyRepository _readRepository;
    private readonly ILoggedUser _loggedUser;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteReservationUseCase(
        IReservationWriteOnlyRepository writeRepository,
        IReservationReadOnlyRepository readRepository,
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
        var user = await _loggedUser.User();

        var reservation = await _readRepository.GetById(id);

        if (reservation is null)
            throw new NotFoundException(ResourceMessagesException.RESERVATION_NOT_FOUND);

        if (user.Id.Equals(reservation.UserId) == false || user.Role.Equals(Role.Admin) == false)
            throw new ForbiddenException();

        await _writeRepository.Delete(id);

        await _unitOfWork.Commit();
    }
}
