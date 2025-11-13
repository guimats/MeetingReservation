using MeetingReservation.Communication.Requests;
using MeetingReservation.Domain.Entities;

namespace MeetingReservation.Application.Extensions.Mapping;

public static class CompanyExtension
{
	public static Company MapToCompany(this RequestRegisterCompanyJson request)
	{
		return new Company
		{
			Name = request.Name
		};
	}
}
