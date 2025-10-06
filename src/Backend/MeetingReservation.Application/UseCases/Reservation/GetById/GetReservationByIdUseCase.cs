using MeetingReservation.Application.Extensions.Mapping;
using MeetingReservation.Communication.Responses;
using MeetingReservation.Domain.Enums;
using MeetingReservation.Domain.Repositories.Reservation;
using MeetingReservation.Domain.Services.LoggedUser;
using MeetingReservation.Exceptions;
using MeetingReservation.Exceptions.ExceptionsBase;

namespace MeetingReservation.Application.UseCases.Reservation.GetById;

public class GetReservationByIdUseCase : IGetReservationByIdUseCase
{
    private readonly IReservationReadOnlyRepository _repository;
    private readonly ILoggedUser _loggedUser;

    public GetReservationByIdUseCase(IReservationReadOnlyRepository repository, ILoggedUser loggedUser)
    {
        _repository = repository;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseLongReservationJson> Execute(long id)
    {
        var user = await _loggedUser.User();

        var reservation = await _repository.GetById(id);

        if (reservation is null)
        {
            throw new NotFoundException(ResourceMessagesException.RESERVATION_NOT_FOUND);
        }

        if (reservation.UserId != user.Id && user.Role != Role.Admin)
            throw new ForbiddenException();

        var response = reservation.MapToLongReservation();

        return response;
    }
}
