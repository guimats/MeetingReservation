using MeetingReservation.Communication.Responses;

namespace MeetingReservation.Application.UseCases.User.GetById;

public interface IGetProfileByIdUseCase
{
    public Task<ResponseUserProfileJson> Execute(long id);
}
