namespace MeetingReservation.Domain.Repositories.Reservation;

public interface IReservationReadOnlyRepository
{
    public Task<Entities.Reservation?> GetById(long reservationId);
}
