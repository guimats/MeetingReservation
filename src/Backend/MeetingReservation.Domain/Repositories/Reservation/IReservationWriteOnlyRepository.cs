namespace MeetingReservation.Domain.Repositories.Reservation;

public interface IReservationWriteOnlyRepository
{
    public Task Add(Entities.Reservation reservation);
    public Task Delete(long id);
}
