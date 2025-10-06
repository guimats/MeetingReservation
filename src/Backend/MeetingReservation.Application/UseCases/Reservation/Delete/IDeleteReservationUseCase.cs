namespace MeetingReservation.Application.UseCases.Reservation.Delete;

public interface IDeleteReservationUseCase
{
    public Task Execute(long id);
}
