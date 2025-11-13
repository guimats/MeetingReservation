using MeetingReservation.Domain.Enums;

namespace MeetingReservation.Communication.Requests;

public class RequestFilterUsersJson
{
	public string? Name { get; set; } = string.Empty;
	public string? Email { get; set; } = string.Empty;
	public Role? Role { get; set; }
}
