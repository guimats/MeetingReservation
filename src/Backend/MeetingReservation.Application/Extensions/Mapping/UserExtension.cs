using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;
using MeetingReservation.Domain.Entities;

namespace MeetingReservation.Application.Extensions.Mapping;

public static class UserExtension
{
    public static User MapToUser(this RequestRegisterUserJson request)
    {
        return new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
            Role = request.Role
        };
    }

	public static User MapToUser(this RequestUpdateUserJson request)
	{
		return new User
		{
			Name = request.Name,
			Email = request.Email,
			Role = request.Role
		};
	}

	public static ResponseUserProfileJson MapToProfile(this User user)
    {
        return new ResponseUserProfileJson
        {
			Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role.ToString()
        };
    }

	public static ResponseUsersJson MapToUsers(this IList<User> users)
	{
		var response = users.Select(user => user.MapToProfile()).ToList();

		return new ResponseUsersJson
		{
			Users = response
		};
	}

	public static RequestRegisterUserJson MapToRegisterUser(this RequestRegisterCompanyJson request)
	{
		return new RequestRegisterUserJson
		{
			Name = request.UserName,
			Password = request.UserPassword,
			Email = request.UserEmail,
			Role = Domain.Enums.Role.Admin
		};
	}
}
