namespace MeetingReservation.Domain.Entities;

public class Reservation : EntityBase, IHaveCompany
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime InitialTime { get; set; }
    public DateTime EndTime { get; set; }
    public int Participants { get; set; }
    public DateTime UpdatedAt { get; set; }
    public long UserId { get; set; }
    public User? User { get; set; }
	public long RoomId { get; set; }
	public Room? Room { get; set; }
	public long CompanyId { get; set; }
}
