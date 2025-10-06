
using MeetingReservation.Domain.Repositories;
using MeetingReservation.Domain.Repositories.Reservation;
using MeetingReservation.Domain.Services.LoggedUser;

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

    public Task Execute(long id)
    {
        throw new NotImplementedException();
    }
}
