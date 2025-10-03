using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;

namespace MeetingReservation.Application.UseCases.User.Register;

public interface IRegisterUserUseCase
{
    public Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
}
