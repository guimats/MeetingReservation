namespace MeetingReservation.Domain.Repositories.Reservation;

public interface IReservationUpdateOnlyRepository
{
	public Task<Entities.Reservation?> GetById(long reservationID, long userID);
	public void Update(Entities.Reservation reservation);
}
