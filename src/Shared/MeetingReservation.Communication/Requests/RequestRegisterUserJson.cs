using MeetingReservation.Domain.Entities;
using MeetingReservation.Domain.Enums;

namespace MeetingReservation.Communication.Requests;

public class RequestRegisterUserJson
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Role Role { get; set; }
    public long CompanyId { get; set; }
}
