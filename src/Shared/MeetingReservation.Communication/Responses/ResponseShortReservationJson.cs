namespace MeetingReservation.Communication.Responses;

public class ResponseShortReservationJson
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Participants { get; set; }
    public DateTime InitialTime { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string RoomName { get; set; } = string.Empty;

}
