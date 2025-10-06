using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;

namespace MeetingReservation.Application.UseCases.Token;

public interface IUseRefreshTokenUseCase
{
    public Task<ResponseTokensJson> Execute(RequestNewTokenJson request);
}

