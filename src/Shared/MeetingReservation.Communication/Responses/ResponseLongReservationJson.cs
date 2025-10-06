namespace MeetingReservation.Communication.Responses;

public class ResponseLongReservationJson
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime InitialTime { get; set; }
    public DateTime EndTime { get; set; }
    public int Participants { get; set; }
    public long UserId { get; set; }
}
