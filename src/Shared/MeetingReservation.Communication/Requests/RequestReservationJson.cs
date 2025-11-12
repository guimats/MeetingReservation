using MeetingReservation.Domain.Entities;

namespace MeetingReservation.Communication.Requests;

public class RequestReservationJson
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime InitialTime { get; set; }
    public DateTime EndTime { get; set; }
    public int Participants { get; set; }
    public long UserId { get; set; }
    public long RoomId { get; set; }
}
