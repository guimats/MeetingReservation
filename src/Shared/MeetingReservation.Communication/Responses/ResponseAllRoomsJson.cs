namespace MeetingReservation.Communication.Responses;

public class ResponseAllRoomsJson
{
	public IList<ResponseShortRoomJson> Rooms { get; set; } = [];
}
