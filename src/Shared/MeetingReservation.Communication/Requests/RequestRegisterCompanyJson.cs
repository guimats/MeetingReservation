namespace MeetingReservation.Communication.Requests;

public class RequestRegisterCompanyJson
{
	public string Name { get; set; } = string.Empty;
	public string UserName { get; set; } = string.Empty;
	public string UserEmail { get; set; } = string.Empty;
	public string UserPassword {  get; set; } = string.Empty;
}
