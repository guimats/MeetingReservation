using MeetingReservation.Domain.Enums;

namespace MeetingReservation.Domain.DTOs;

public class FilterUsersDTO
{
	public string? Name { get; set; } = string.Empty;
	public string? Email { get; set; } = string.Empty;
	public Role? Role { get; set; }
}
