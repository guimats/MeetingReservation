using MeetingReservation.Domain.DTOs;

namespace MeetingReservation.Domain.Repositories.Reservation;

public interface IReservationReadOnlyRepository
{
    public Task<Entities.Reservation?> GetById(long reservationId);
	public Task<IList<Entities.Reservation>> Filter(FilterReservationsDTO filter);

}
