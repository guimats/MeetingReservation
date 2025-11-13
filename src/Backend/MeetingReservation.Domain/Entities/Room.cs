namespace MeetingReservation.Domain.Entities
{
	public class Room : EntityBase
	{
		public string Name { get; set; } = string.Empty;
		public int Capacity { get; set; }
		public string Location { get; set; } = string.Empty;
		public long UserId { get; set; }
		public User? User { get; set; }
		public long CompanyId { get; set; }

		public IList<Reservation> Reservations { get; set; } = new List<Reservation>();
	}
}
