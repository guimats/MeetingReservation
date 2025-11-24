using MeetingReservation.Domain.Entities;

namespace MeetingReservation.Communication.Responses;

public class ResponseShortCompanyJson
{
	public long Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public ResponseUsersJson Users { get; set; } = new ResponseUsersJson();
}
