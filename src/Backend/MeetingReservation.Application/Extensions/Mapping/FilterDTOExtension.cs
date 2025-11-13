using MeetingReservation.Communication.Requests;
using MeetingReservation.Domain.DTOs;

namespace MeetingReservation.Application.Extensions.Mapping;

public static class FilterDTOExtension
{
	public static FilterRoomsDTO MapToFilter(this RequestFilterRoomsJson request)
	{
		return new FilterRoomsDTO
		{
			Name = request.Name,
			Capacity = request.Capacity,
			Location = request.Location
		};
	}

	public static FilterUsersDTO MapToFilter(this RequestFilterUsersJson request)
	{
		return new FilterUsersDTO
		{
			Name = request.Name,
			Email = request.Email,
			Role = request.Role
		};
	}
}
