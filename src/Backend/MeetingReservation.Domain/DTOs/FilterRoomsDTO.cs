namespace MeetingReservation.Domain.DTOs
{
	public class FilterRoomsDTO
	{
		public string? Name { get; set; } = string.Empty;
		public int? Capacity { get; set; }
		public string? Location { get; set; } = string.Empty;
	}
}
