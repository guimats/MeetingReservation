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

    public static ResponseUserProfileJson MapToProfile(this User user)
    {
        return new ResponseUserProfileJson
        {
            Name = user.Name,
            Email = user.Email,
            Role = user.Role.ToString()
        };
    }
}
