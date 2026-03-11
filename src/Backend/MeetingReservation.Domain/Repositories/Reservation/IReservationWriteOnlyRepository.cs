using MeetingReservation.Domain.DTOs;

namespace MeetingReservation.Domain.Repositories.Reservation;

public interface IReservationWriteOnlyRepository
{
    public Task Add(Entities.Reservation reservation);
    public Task Delete(long id);
    public Task<bool> IsTimeOccupied(long roomId, DateTime initialTime, DateTime endTime);
}
