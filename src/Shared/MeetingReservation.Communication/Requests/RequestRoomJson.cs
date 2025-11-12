namespace MeetingReservation.Communication.Requests;

public class RequestRoomJson
{
	public string Name { get; set; } = string.Empty;
	public int Capacity { get; set; }
	public string Location { get; set; } = string.Empty;
	public long UserId { get; set; }
}