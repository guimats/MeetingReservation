namespace MeetingReservation.Communication.Requests;

public class RequestFilterReservationsJson
{
	public string? Name { get; set; } = string.Empty;
	public string? Description { get; set; } = string.Empty;
	public int? MinParticipants { get; set; }
}
