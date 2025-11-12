namespace MeetingReservation.Communication.Responses;

public class ResponseShortRoomJson
{
	public long Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public int Capacity { get; set; }
	public string Location { get; set; } = string.Empty;
}
