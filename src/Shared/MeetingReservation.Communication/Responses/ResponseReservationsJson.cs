namespace MeetingReservation.Communication.Responses;

public class ResponseReservationsJson
{
	public IList<ResponseShortReservationJson> Reservations { get; set; } = [];
}
