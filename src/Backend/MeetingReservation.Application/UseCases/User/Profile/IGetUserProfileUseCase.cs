using MeetingReservation.Communication.Responses;

namespace MeetingReservation.Application.UseCases.User.Profile;

public interface IGetUserProfileUseCase
{
    public Task<ResponseUserProfileJson> Execute();
}
