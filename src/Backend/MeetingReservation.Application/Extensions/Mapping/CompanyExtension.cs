using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;
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

	public static ResponseShortCompanyJson MapToShortRequest(this Company company)
	{
		return new ResponseShortCompanyJson
		{
			Id = company.Id,
			Name = company.Name,
			Users = company.Users.Count
		};
	}
	public static Company MapToCompany(this RequestUpdateCompanyJson request, Company company)
	{
		company.Name = request.Name;

		return company;
	}
}
